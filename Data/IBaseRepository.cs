using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using sample1.Models;

namespace sample1.Data
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
        
        TEntity GetById(int id);

        void Add(TEntity entity);

        void Update(TEntity entity);
        
        void Delete(TEntity entity);
    }
}