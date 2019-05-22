using System;
using System.Linq;
using System.Linq.Expressions;

namespace ITISKeys.Infrastructure.Repositories
{
    interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        void Update(TEntity entityToUpdate);

        void Delete(TEntity entityToDelete);
    }
}
