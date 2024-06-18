

using System.Runtime.CompilerServices;
using CompetenceProfilingDomain.Contracts;
using CompetenceProfilingDomain.Contracts.models;
using Microsoft.EntityFrameworkCore;

namespace CompetenceProfilingInfrastructure.Data
{


    public class DatabaseContext : DbContext,IRepository
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.Migrate();
        }
     
        public DbSet<StudentAdvice> StudentAdvices { get; set; } = null!;

        public DbSet<StudentKpi> StudentKpi { get; set; } = null!;
        
        
        void IRepository.Add<TEntity>(TEntity entity)
        {
            Add<TEntity>(entity);
        }

        void IRepository.Update<TEntity>(TEntity entity)
        {
            Update<TEntity>(entity);
        }

        void IRepository.Remove<TEntity>(TEntity entity)
        {
            Remove<TEntity>(entity);
        }
        
        IQueryable<TEntity> IRepository.Query<TEntity>()
        {
            return Set<TEntity>();
        }

        TEntity IRepository.GetById<TEntity, TId>(TId id)
        {
            var entity = Set<TEntity>().Find(id);
            if (entity == null)
                throw new InvalidOperationException($"No Entity {typeof(TEntity).Name} found for id {id}");
            return entity;
        }

        int IRepository.SaveChanges()
        {
            return SaveChanges();
        }
        
       
    }
}
