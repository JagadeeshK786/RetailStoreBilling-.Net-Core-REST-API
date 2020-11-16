using StoreDAC;
using StoreDAC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreDAC.Repositories;
using System.Threading.Tasks;
using StoreDAC.DBContext;

namespace StoreServices.Repo
{
    public class ProdCategoryRepository : BaseRepository, IProdCategoryRepository
    {        
        public ProdCategoryRepository(RetailStoreDBContext context) : base(context)
        {}

        public async Task<IEnumerable<ProductCategory>> ListAsync()
        {
             return await _context.ProductCategories.AsNoTracking().ToListAsync();
        }

        public async Task<ProductCategory> FindByIdAsync(long id)
        {
            return await _context.ProductCategories.Include(category => category.Products).FirstOrDefaultAsync(cat => cat.CategoryId == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.ProductCategories.CountAsync();
        }
    }
}
