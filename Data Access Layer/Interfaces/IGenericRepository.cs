using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string Name);
        Task<T> GetByIdAsync(int id, string[]? Includes = null);
        Task<T> GetData_ByExepressionAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task Add(T entity);
        T Delete(T entity);
        T Update(T entity);
    }
}
