using AzureSampleDataAccessLayer.Interfaces;
using AzureSampleDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSampleDataAccessLayer.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDBContext context) : base(context) { }

        public async Task<Product> GetByProductNumberAsync(string productNumber)
            => await _dbSet.FirstOrDefaultAsync(p => p.ProductNumber == productNumber);

        public async Task<IEnumerable<Product>> SearchAsync(string name, string category, bool inStockOnly)
            => await _dbSet
                    .Include(p => p.ProductCategory)
                        .ThenInclude(pc => pc.ParentProductCategory)
                    .Where(p => 
                        (string.IsNullOrWhiteSpace(name) 
                            || p.Name.Contains(name))
                        && (string.IsNullOrWhiteSpace(category) 
                            || (null != p.ProductCategory 
                                && null != p.ProductCategory.ParentProductCategory
                                && p.ProductCategory.ParentProductCategory.Name == category))
                        && (!inStockOnly 
                            || (DateTime.Now >= p.SellStartDate 
                                && (null == p.SellEndDate || DateTime.Now <= p.SellEndDate))))
                    .ToListAsync();
    }
}
