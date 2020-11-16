using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreServices
{
    public interface IProdCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        Task<ProductCategory> GetCategoryByIdAsync(long Id);
    }
}
