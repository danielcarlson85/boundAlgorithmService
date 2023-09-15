using AlgorithmService.IoTHubFunctions.Entities;
using System.Data.Entity;

namespace Bound.AlgorithmService.Managers.Database
{
    public class SqlContext : DbContext
    {
        public SqlContext() : base ("name=BoundUserDb")
        {

        }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);
        }

    }
}
