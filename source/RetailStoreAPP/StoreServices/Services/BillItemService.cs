using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using StoreDAC.Repositories;
using StoreServices.Infrastructure;
using StoreDAC.Entities;
using System.Linq;
using StoreServices.ServiceModels;

namespace StoreServices
{
    public class BillItemService : IBillItemService
    {
        public BillItemService(IBillItemRepository billItemRepo, IBillRepository billIRepo, IProductRepository productRepo, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _billItemRepo = billItemRepo;
            _billRepo = billIRepo;
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
                
        public async Task<IEnumerable<BillItem>> GetBillItemsAsync()
        {
            //caching
            var items = await _cache.GetOrCreateAsync(CacheKeys.BillItemsList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _billItemRepo.ListDetailAsync(ItemStatus.INPROGRESS.ToString());

            });

            //output validation
            if (items == null || !items.Any())
                throw new Exception($"No BillIs exist.");

            return items;
        }
        public async Task<BillItem> GetBillItemByIdAsync(long itemId)
        {
            var billItem = await _billItemRepo.FindByIdAsync(itemId);

            if (billItem == null)
                throw new Exception($"No BillItem exists with Bill No: {itemId}");

            return billItem;
        }

        public async Task UpdateAsync(BillItemUpdateSM itemModel)
        {
            var billItem = await _billItemRepo.FindByIdAsync(itemModel.ItemId);

            if(billItem.Status != ItemStatus.INPROGRESS.ToString())
                throw new Exception($"The BillItem '{itemModel.ItemId}' cannot be updated due to it's current status: {billItem.Status}");

            billItem.ProductId = itemModel.ProductId;
            billItem.Quantity = itemModel.Quantity;
            _billItemRepo.Update(billItem);

            BillService billService = new BillService(_billRepo,_productRepo,_unitOfWork,null);
            var bill = await _billRepo.FindBillStatusByIdAsync(itemModel.BillId);

            await billService.SaveAsync(bill);
        }

        public async Task<BillItem> CancelAsync(BillItem billItem)
        {
            if (billItem == null)
                throw new Exception($"No BillItem exists to update, with Id: {billItem.Sno}");

            billItem.Status = BillStatus.CANCELLED.ToString();
            _billItemRepo.Update(billItem);
            await RecalcBill(billItem.BillId);

            await _unitOfWork.CompleteAsync();
            return billItem;
        }
        private async Task RecalcBill(long billId)
        {
            var bill = await _billRepo.FindBillStatusByIdAsync(billId);

            bill.TotalAmount = bill.BillItems
                .Where(od => od.Status.Equals(BillStatus.INPROGRESS.ToString()))
                .Sum(a => a.NetPrice);

            bool hasAnyChildsLeft = bill.BillItems.Where(od => (od.Status.Equals(BillStatus.INPROGRESS.ToString())
                                    || od.Status.Equals(BillStatus.READONLY.ToString())))
                                    .Any();
            if (!hasAnyChildsLeft)
                bill.BillStatus = BillStatus.CANCELLED.ToString();

            _billRepo.Update(bill);
        }

        private readonly IBillItemRepository _billItemRepo;
        private readonly IBillRepository _billRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
    }
}
