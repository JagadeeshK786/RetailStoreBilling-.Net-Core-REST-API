using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreDAC.Repositories
{
    public interface IBillItemRepository
    {
        Task<BillItem> FindByIdAsync(long itemId);
        Task<IEnumerable<BillItem>> ListAsync(string billStatus);
        public void Update(BillItem billItem);

    }
}
