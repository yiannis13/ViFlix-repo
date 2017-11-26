using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ViFlix.Repository.Contract
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        Task<T> GetAsync(object id);

        Task<IList<T>> GetAllAsync();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Remove(T entity);
    }
}