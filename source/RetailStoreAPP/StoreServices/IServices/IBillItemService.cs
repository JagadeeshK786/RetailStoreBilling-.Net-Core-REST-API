using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;
using StoreServices.ServiceModels;

namespace StoreServices
{
    public interface IBillItemService
    {
        Task<BillItem> GetBillItemByIdAsync(long itemId);
        Task<IEnumerable<BillItem>> GetBillItemsAsync();
        Task UpdateAsync(BillItemUpdateSM itemModel);
        Task<BillItem> CancelAsync(BillItem item);
    }
}
