namespace CompetenceProfilingDomain.Contracts.Infrastructure;

public interface IRepository
{
    // https://stackoverflow.com/questions/62265187/creating-an-interface-for-an-entity-framework-core-dbcontext-so-that-it-can-be-i
    
    void Add<TEntity>(TEntity entity) where TEntity : class;
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    TEntity GetById<TEntity, TId>(TId id) where TEntity : class;
    int SaveChanges();
}