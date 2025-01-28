using Domain.Entities;

namespace Domain.RepositoryContracts
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task EditAsync(TEntity entityToUpdate);
        void Remove(TKey id);
        void Remove(TEntity entityToDelete);
    }
}
