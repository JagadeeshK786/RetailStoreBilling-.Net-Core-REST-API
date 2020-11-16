using StoreDAC;
using StoreDAC.Entities;
using Microsoft.EntityFrameworkCore;
using StoreDAC.Repositories;
using System.Threading.Tasks;
using StoreDAC.DBContext;
using System.Collections.Generic;
using System.Linq;

namespace StoreServices.Repo
{
    public class BillItemRepository : BaseRepository, IBillItemRepository
    {        
        public BillItemRepository(RetailStoreDBContext context) : base(context)
        {}

        public async Task<IEnumerable<BillItem>> ListAsync(string status)
        { 
            return await _context.BillItems.Where(b => b.Status == status).AsNoTracking().ToListAsync();
        }

        public async Task<BillItem> FindByIdAsync(long itemId)
        {
            return await _context.BillItems.Include(o=>o.Product).FirstOrDefaultAsync(b => b.Sno == itemId);
        }
        
        public async Task<IEnumerable<BillItem>> ListDetailAsync(string status)
        {
            return await _context.BillItems.Include(o => o.Product).Where(b => b.Status == status).AsNoTracking().ToListAsync();
        }
        
        public void Update(BillItem billItem)
        {
            _context.Entry(billItem).State = EntityState.Modified;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Bills.CountAsync();
        }
    }
}
