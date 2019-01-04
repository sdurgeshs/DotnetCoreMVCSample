using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using sample1.Models;

namespace sample1.Data
{
    public class MyContext: DbContext, IDbContext  
    {
        public MyContext (DbContextOptions<MyContext> options)  
            : base(options)  
        {  
        }  
  
        #region DbSets

        public DbSet<Employee> Employee { get; set; }  
        public DbSet<Department> Department { get; set; }  
        public DbSet<EmployeeActivity> EmployeeActivity { get; set; }

        #endregion

        #region IDbContext

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void BeginTransaction()
        {
            this.BeginTransaction();
        }

        public void Rollback()
        {
            this.Rollback();
        }

        public int Commit()
        {
            try
            {
                BeginTransaction();
                var saveChanges = SaveChanges();
                this.Commit();

                return saveChanges;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private EntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        #endregion
    }  
}