using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        void Delete(T entity);
        void AddRange(IEnumerable<T> entities);
    }
}
