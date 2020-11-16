using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreServices
{
    public interface IBillService
    {
        Task<Bill> GetBillByIdAsync(long billId);
        Task<Bill> GetBillStatusByIdAsync(long billId);
        Task<IEnumerable<Bill>> GetBillsAsync();       
        Task<Bill> SaveAsync(Bill bill, bool isCreate /*= true*/);
        Task<Bill> UpdateBillStatusAsync(Bill bill, string status);
        //Task<Bill> UpdateBillStatusAsync(long billId, string status, bool IsCancel = false);

    }
}
