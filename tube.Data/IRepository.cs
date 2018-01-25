using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace tube.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //TEntity FindById(int id, params Expression<Func<TEntity, object>>[] includeProperties);

        //TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        //IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        //void Add(TEntity entity);

        void Update(TEntity entity);

        //void Remove(TEntity entity);

        //void Remove(int id);

        //void RemoveMultiple(List<TEntity> entities);
        IQueryable<TEntity> GetQueryable();
    }
}
