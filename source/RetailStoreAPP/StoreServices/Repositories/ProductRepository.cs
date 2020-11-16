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
    public class ProductRepository : BaseRepository, IProductRepository
    {        
        public ProductRepository(RetailStoreDBContext context) : base(context)
        {}

        public async Task<IEnumerable<Product>> ListAsync()
        {
             return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> FindByIdAsync(long sno)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.SerialNumber == sno);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
        
        ////public async Task AddAsync(Tasklist newTask)
        ////{
        ////    await _context.Tasklist.AddAsync(newTask);
        ////}

        //public async Task<ProductCategory> FindByIdAsync(string id)
        //{
        //    throw new NotImplementedException();

        //    //return await _context.Tasklist.FindAsync(id);
        //}



        ////public void Remove(Tasklist role)
        ////{
        ////    _context.Tasklist.Remove(role);
        ////}
        //public async Task<int> GetCountAsync()
        //{
        //    return await _context.Tasklist.CountAsync();
        //}

        ////public async Task<IList<KeyValuePair<int, int>>>
        ////public IEnumerable<TeamTaskMetric> ListTeamMetricsAsync()
        ////{
        ////    //SELECT Tasks => groupby team => where status = OPEN 
        ////  var taskGroupResult =  _context.Tasklist.Where(s=>s.Status == 101).GroupBy(t => t.TeamId).Select(x=> new TeamTaskMetric { teamId = x.Key, tasksCount = x.Count() });
        ////  return taskGroupResult;
        ////}

        // public Task<ProductCategory> GetCategoryByIdAsync(long id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
