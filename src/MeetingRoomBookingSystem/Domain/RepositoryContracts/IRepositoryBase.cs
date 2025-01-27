using Domain.Entities;

namespace Domain.RepositoryContracts
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        Task AddAsync(TEntity entity);
    }
}
