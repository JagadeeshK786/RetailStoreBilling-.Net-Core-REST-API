using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreDAC.Repositories
{
    public interface IProdCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> ListAsync();
        Task<ProductCategory> FindByIdAsync(long id);

        //Task AddAsync(ProductCategory newTask);
       
        //void Update(ProductCategory task);
        //void Remove(ProductCategory task);
        //IEnumerable<TeamTaskMetric> ListTeamMetricsAsync();
    }
}
