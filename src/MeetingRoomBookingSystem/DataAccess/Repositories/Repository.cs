using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;

namespace DataAccess.Repositories
{
    public abstract class Repository<TEntity, TKey>
        : IRepository<TEntity, TKey> where TKey : IComparable
        where TEntity : class, IEntity<TKey>
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task EditAsync(TEntity entityToUpdate)
        {
            await Task.Run(() =>
            {
                _dbSet.Attach(entityToUpdate);
                _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic( //this is the code
           Expression<Func<TEntity, bool>> filter = null,
           string orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
        }
    }
}
