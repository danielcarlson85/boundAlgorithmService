using AutoMapper;
using Bound.AlgorithmService.Managers.Database;
using AlgorithmService.Abstractions.Dtos;
using AlgorithmService.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgorithmService.Abstractions.Managers
{
    public abstract class BaseManager<TEntity, TYRequest> : IBaseManager<TEntity, TYRequest>
        where TEntity : BaseEntity
        where TYRequest : DtoBase
    {
        private readonly SqlContext _sqlContext;
        private readonly IMapper _mapper;

        protected BaseManager(SqlContext sqlContext, IMapper mapper)
        {
            _sqlContext = sqlContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the specified entity.
        /// </summary>
        /// <param name="id">The id of the entity to return.</param>
        public abstract Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Gets all entities of type TEntity.
        /// </summary>
        public abstract Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Creates a new entity of type TEntity.
        /// </summary>
        /// <param name="request">The request object to create.</param>
        public async Task<TEntity> CreateAsync(TYRequest request)
        {
            await Validate(request);
            var dbEntity = _mapper.Map<TEntity>(request);
            dbEntity.Created = DateTime.UtcNow;

            dbEntity = await SetReferences(dbEntity, request);

            var entityEntry = _sqlContext.Set<TEntity>().Add(dbEntity);
            await _sqlContext.SaveChangesAsync();

            await PublishCreated(entityEntry);

            return entityEntry;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="request">The request object to update.</param>
        public async Task<TEntity> UpdateAsync(int id, TYRequest request)
        {
            if (id != request.Id)
            {
                throw new ArgumentException("Id in URL and request object differs.");
            }

            await Validate(request);

            var dbEntity = await GetAsync(id);

            if (dbEntity == null)
            {
            }

            _mapper.Map(request, dbEntity);
            dbEntity.Updated = DateTime.UtcNow;

            dbEntity = await SetReferences(dbEntity, request);

            var entityEntry = _sqlContext.Set<TEntity>().Attach(dbEntity);
            await _sqlContext.SaveChangesAsync();

            await PublishUpdated(entityEntry);

            return entityEntry;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <param name="dbEntity">Entity to delete, optional.</param>
        public async Task DeleteAsync(int id, TEntity dbEntity = null)
        {
            if (dbEntity == null)
            {
                dbEntity = await _sqlContext.Set<TEntity>().FindAsync(id);
            }

            if (dbEntity == null)
            {
            }

            _sqlContext.Set<TEntity>().Remove(dbEntity);
            await _sqlContext.SaveChangesAsync();
        }

        public abstract Task<TEntity> SetReferences(TEntity entity, TYRequest request);

        public abstract Task Validate(TYRequest request);

        public abstract Task PublishCreated(TEntity entity);

        public abstract Task PublishUpdated(TEntity entity);
    }
}
