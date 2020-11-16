using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreDAC.Repositories
{
    public interface IBillRepository
    {
        Task<Bill> FindByIdAsync(long billId);
        Task<Bill> FindBillStatusByIdAsync(long billId);
        Task<IEnumerable<Bill>> ListAsync();
        Task<Bill> AddAsync(Bill bill);
        void Update(Bill bill);
        void Remove(Bill bill);
    }
}
