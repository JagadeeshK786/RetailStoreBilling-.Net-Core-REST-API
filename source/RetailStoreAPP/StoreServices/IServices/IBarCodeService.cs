using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreServices
{
    public interface IBarCodeService
    {
        Task<long> GetSerialNoByBarCodeAsync(string barCode);
        Task<IEnumerable<Barcode>> GetBarCodesAsync();
    }
}
