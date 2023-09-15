using AlgorithmService.Abstractions.Dtos;
using AlgorithmService.Abstractions.Entities;

namespace AlgorithmService.Abstractions.Managers
{
    public interface IBaseManager<TEntity, TYRequest>
        where TEntity : BaseEntity
        where TYRequest : DtoBase
    {
    }
}