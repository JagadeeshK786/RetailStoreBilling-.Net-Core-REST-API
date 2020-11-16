using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreDAC.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindByIdAsync(long sno);
        Task<IEnumerable<Product>> ListAsync();
        void Update(Product product);

        //Task AddAsync(ProductCategory newTask);       
        //void Update(ProductCategory task);
        //void Remove(ProductCategory task);
        //IEnumerable<TeamTaskMetric> ListTeamMetricsAsync();
    }
}
