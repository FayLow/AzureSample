using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSampleDataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetAll();
        //T GetById(int id);
        //int Add(T entity);
        //int Update(T entity);
        //int Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
