using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SpyStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpyStore.DAL.Repos.Base
{
    public abstract class RepoBase<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        protected StoreContext Db { get; }
        protected DbSet<T> Table { get; set; }
        private bool _disposed = false;    

        protected RepoBase()
        {
            Db = new StoreContext();
            Table = Db.Set<T>();
        }

        protected RepoBase(DbContextOptions<StoreContext> options)
        {
            Db = new StoreContext(options);
            Table = Db.Set<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

            }

            Db.Dispose();
            _disposed = true;
        }

        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entry = GetEntryFromChangeTracker(id);
            if (entry != null)
            {
                if (entry.Timestamp == timeStamp)
                {
                    return Delete(entry, persist);
                }

                throw new Exception("Unable to delete due to concurremcy violation");
            }

            Db.Entry(new T { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public T Find(int? id)
        {
            return Table.Find(id);
        }

        public T GetFirst()
        {
            return Table.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Table;
        }

        public virtual IEnumerable<T> GetRange(int skip, int take)
        {
            return GetRange(Table, skip, take);
        }

        public StoreContext Context => Db;

        public int Count => Table.Count();

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        internal T GetEntryFromChangeTracker(int? id)
        {
            return Db.ChangeTracker.Entries<T>().Select((EntityEntry e) => (T)e.Entity)
                .FirstOrDefault(x => x.Id == id);
        }

        internal IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }
    }
}
