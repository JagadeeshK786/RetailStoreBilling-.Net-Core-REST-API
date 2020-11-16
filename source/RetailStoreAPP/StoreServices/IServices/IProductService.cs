using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreServices
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(long sno);
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
