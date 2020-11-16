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
    public class BarcodeRepository : BaseRepository, IBarcodeRepository
    {        
        public BarcodeRepository(RetailStoreDBContext context) : base(context)
        {}

        public async Task<IEnumerable<Barcode>> ListAsync()
        {
             return await _context.Barcodes.AsNoTracking().ToListAsync();
        }

        public async Task<Barcode> FindByIdAsync(string barcode)
        {
            return await _context.Barcodes.FirstOrDefaultAsync(p => p.BarcodeId.Equals(barcode.Trim()));
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Barcodes.CountAsync();
        }
    }
}
