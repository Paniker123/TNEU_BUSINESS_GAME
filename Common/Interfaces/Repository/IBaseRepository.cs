using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface IBaseRepository<T, in TK>
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(TK id);
        Task<IEnumerable<T>> Select(Func<T, bool> predicate);
        Task<T> Find(Func<T, bool> predicate);
        void Create(params T[] items);
        void Update(T item);
        void Delete(TK id);
        bool Save();
        Task<bool> SaveAsync();
    }
}
