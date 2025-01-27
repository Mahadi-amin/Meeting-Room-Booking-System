using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.RepositoryContracts
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {

    }
}
