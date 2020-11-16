using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using StoreDAC.Repositories;
using StoreServices.Infrastructure;
using StoreDAC.Entities;
using System.Linq;

namespace StoreServices
{
    public class ProductService : IProductService
    {
        public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            //caching
            var products = await _cache.GetOrCreateAsync(CacheKeys.ProductList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepo.ListAsync();
            });
            //output validation
            if (products == null || !products.Any())
                throw new Exception($"No Products exist.");

            return products;
        }

        public async Task<Product> GetProductByIdAsync(long sno)
        {
            var product = await _productRepo.FindByIdAsync(sno);

            if (product == null)
                throw new Exception($"No Product exists with SerialNo: {sno}");

            return product;
        }


        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

    }
}
