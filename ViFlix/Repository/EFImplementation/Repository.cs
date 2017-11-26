using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ViFlix.Repository.Contract;

namespace ViFlix.Repository.EFImplementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public async Task<T> GetAsync(object id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}