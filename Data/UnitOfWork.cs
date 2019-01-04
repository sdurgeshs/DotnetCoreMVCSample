using System;
using System.Collections;
using sample1.Models;

namespace sample1.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly MyContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(MyContext context)
        {
            _context = context;
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
            
            if (_repositories.ContainsKey(type))
            {
                return (IBaseRepository<TEntity>)_repositories[type];
            }
            
            var repositoryType = typeof(BaseRepository<>);
            
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
            
            return (IBaseRepository<TEntity>)_repositories[type];
        }

        public int Commit()
        {
            return _context.Commit();
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
                foreach (IDisposable repository in _repositories.Values)
                {
                    repository.Dispose();
                }
            }
            _disposed = true;
        }
        
    }
}
