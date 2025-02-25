using AzureSampleDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSampleDataAccessLayer.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByProductNumberAsync(string productNumber);
        Task<IEnumerable<Product>> SearchAsync(string name, string category, bool inStockOnly);
    }
}
