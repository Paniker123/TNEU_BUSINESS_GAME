using Common.Interfaces.Repository;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly MSContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(MSContext context, DbSet<TEntity> dataSet)
        {
            Db = context;
            DbSet = dataSet;
        }

        public bool Exist(Func<TEntity, bool> predicate)
        {
            return DbSet.Any(predicate);
        }

        public void Delete(TKey id)
        {
            var item = DbSet.Find(id);
            if (item != null)
                DbSet.Remove(item);
        }

        public bool Save()
        {
            try
            {
                return Db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(TEntity item)
        {
            if (item == null) return;
            Db.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = Db.ChangeTracker.Entries()
                    .Count(p => p.State == EntityState.Modified || p.State == EntityState.Deleted || p.State == EntityState.Added);
                if (changes == 0) return true;
                return await Db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task<DbOperationResult<bool>> SaveWithLogAsync()
        //{
        //    var currentTransaction = Db.Database.CurrentTransaction ?? Db.Database.BeginTransaction();

        //    DbOperationResult<bool> result;

        //    try
        //    {
        //        var saveChanges = await Db.SaveChangesAsync();
        //        if (saveChanges == 0)
        //        {
        //            const string description = "Could not save data to the database";
        //            throw new Exception(description);
        //        }

        //        currentTransaction.Commit();
        //        result = new DbOperationResult<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        currentTransaction.Rollback();
        //        result = new DbOperationResult<bool>(DbErrorType.DbExceptionError, ex);
        //    }
        //    finally
        //    {
        //        currentTransaction.Dispose();
        //    }

        //    return result;
        //}

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Select(Func<TEntity, bool> predicate)
        {
            var result = Task.FromResult(DbSet.Where(predicate));
            return (await result).ToList();
        }

        public async Task<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var result = Task.FromResult(DbSet.Where(predicate));
            return (await result).FirstOrDefault();
        }

        public async Task<ICollection<TEntity>> FindManyAsync(Func<TEntity, bool> predicate)
        {
            var user = Task.FromResult(DbSet.Where(predicate));
            return (await user).ToList();
        }

        public async Task<TEntity> FindFirstAsync(Func<TEntity, bool> predicate)
        {
            var user = Task.FromResult(DbSet.FirstOrDefault(predicate));
            return await user;
        }

        public void Create(params TEntity[] items)
        {
            DbSet.AddRange(items);
        }
    }
}