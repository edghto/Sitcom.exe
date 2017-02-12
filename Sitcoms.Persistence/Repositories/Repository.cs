using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Sitcoms.Persistence.Repositories
{
    public class Repository<TEntity>
        : Core.Repositories.IRepository<TEntity>
        where TEntity : class
    {
        protected DbContext Context { get; private set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entry)
        {
            Context.Set<TEntity>().Add(entry);
        }

        public void AddRange(IEnumerable<TEntity> entries)
        {
            Context.Set<TEntity>().AddRange(entries);
        }

        public void Remove(TEntity entry)
        {
            Context.Set<TEntity>().Remove(entry);
        }

        public void RemoveRange(IEnumerable<TEntity> entries)
        {
            Context.Set<TEntity>().RemoveRange(entries);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
    }
}
