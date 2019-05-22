using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ITISKeys.Infrastructure.Context;

namespace ITISKeys.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ITISKeysDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ITISKeysDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public virtual void Update(TEntity entityToUpdate) =>
            _context.Attach(entityToUpdate).State = EntityState.Modified;

        public virtual void SetNull(TEntity entity, string propertyName) =>
            _context.Entry(entity).Property(propertyName).CurrentValue = null;

        public virtual void Delete(TEntity entityToDelete) => _dbSet.Remove(entityToDelete);

    }
}
