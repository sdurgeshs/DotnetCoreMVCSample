using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using sample1.Models;

namespace sample1.Data
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MyContext _context;
        private bool _disposed;

        public BaseRepository(MyContext context)
        {
            _context = context;
        }
        
        public IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return this._context.Set<TEntity>().Where(expression);
        }

        public TEntity GetById(int id)
        {
            return this._context.Set<TEntity>().SingleOrDefault(s => s.Id == id);
        }

        public void Add(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            this._context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this._context.Set<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
