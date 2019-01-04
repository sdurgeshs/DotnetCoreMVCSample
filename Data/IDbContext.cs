using System;
using Microsoft.EntityFrameworkCore;
using sample1.Models;

namespace sample1.Data
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity;
        
        void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity;
        
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity;
        
        void BeginTransaction();
        
        int Commit();
        
        void Rollback();
    }
}