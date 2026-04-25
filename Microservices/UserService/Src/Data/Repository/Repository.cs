using Domain.Common;
using Domain.Interfaces;

namespace Data.Repository;

public class Repository : IRepository
{
    public Task Add<TEntity>(TEntity entity) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task Delete<TEntity>(TEntity entity) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetAll<TEntity>() where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetForId<TEntity>(Guid id, params string[] included) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetTheFirstOne<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] included) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> List<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] included) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> ListAll<TEntity>(params string[] included) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> ListAllWith<TEntity>(params Expression<Func<TEntity, object>>[] included) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }

    public Task Update<TEntity>(TEntity entity) where TEntity : EntityBase
    {
        throw new NotImplementedException();
    }
}
