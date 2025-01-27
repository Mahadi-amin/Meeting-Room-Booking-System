using Domain.Entities;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {

    }
}
