using System;
using sample1.Models;

namespace sample1.Data
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        
        int Commit();
        
        void Dispose(bool disposing);
    }
}
