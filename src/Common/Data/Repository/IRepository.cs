using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T model);

        Task<T> GetAsync(object id);

        Task<IList<T>> GetAllAsync();

        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Remove(T model);
    }
}