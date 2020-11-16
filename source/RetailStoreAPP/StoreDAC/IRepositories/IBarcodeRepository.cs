using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreDAC.Repositories
{
    public interface IBarcodeRepository
    {
        Task<Barcode> FindByIdAsync(string barcode);
        Task<IEnumerable<Barcode>> ListAsync();
    }
}
