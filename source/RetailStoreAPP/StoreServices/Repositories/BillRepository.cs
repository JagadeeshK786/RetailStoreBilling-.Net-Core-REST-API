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
    public class BillRepository : BaseRepository, IBillRepository
    {        
        public BillRepository(RetailStoreDBContext context) : base(context)
        {}

        public async Task<IEnumerable<Bill>> ListAsync()
        {
            return await _context.Bills.AsNoTracking().ToListAsync();
        }

        public async Task<Bill> FindByIdAsync(long billId)
        {
            return await _context.Bills.Include(o => o.BillItems).ThenInclude(od => od.Product).FirstOrDefaultAsync(o => o.BillId == billId);
        }

        public async Task<Bill> FindBillStatusByIdAsync(long billId)
        {
            return await _context.Bills.Include(o => o.BillItems).FirstOrDefaultAsync(o => o.BillId == billId);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Bills.CountAsync();
        }

        public void Update(Bill bill)
        {
           _context.Entry(bill).State = EntityState.Modified;
        }

        public void Remove(Bill bill)
        {
            throw new NotImplementedException();
        }
        public async Task<Bill> AddAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            return bill;
        }

        
    }
}
