using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using StoreDAC.Repositories;
using StoreServices.Infrastructure;
using StoreDAC.Entities;
using System.Linq;

namespace StoreServices
{
    public class BillService : IBillService
    {
        public BillService(IBillRepository billRepo, IProductRepository prodRepo,IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _billRepo = billRepo;
            _prodRepo = prodRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            //caching
            var bills = await _cache.GetOrCreateAsync(CacheKeys.BillsList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _billRepo.ListAsync();
            });

            //output validation
            if (bills == null || !bills.Any())
                throw new Exception($"No BillIs exist.");

            return bills;
        }

        public async Task<Bill> GetBillByIdAsync(long billId)
        {
            var bill = await _billRepo.FindByIdAsync(billId);

            if (bill == null)
                throw new Exception($"No Bill exists with Bill No: {billId}");

            return bill;
        }

        public async Task<Bill> GetBillStatusByIdAsync(long billId)
        {
            var bill = await _billRepo.FindBillStatusByIdAsync(billId);

            if (bill == null)
                throw new Exception($"No Bill exists with Bill No: {billId}");

            return bill;
        }
      
        public async Task<Bill> SaveAsync(Bill bill, bool isCreate = false)
        {
            if (bill.BillItems.Count <= 0)
            {
                throw new Exception($"One or More Bill items must exist to proceed with Bill creation.");
            }
            bill.BillItems.ToList().ForEach(a =>
            {
                var prod = _prodRepo.FindByIdAsync(a.ProductId).Result;
                if (prod == null)
                    throw new KeyNotFoundException($"No Product exists with ID: {a.ProductId}");
                a.Price = a.Quantity * prod.UnitPrice;
                a.NetPrice = a.Price * (decimal)((100.0 - prod.Discount) / 100);
                a.Status = a.Status ?? ItemStatus.INPROGRESS.ToString();
            });
            bill.TotalAmount = bill.BillItems.Where(bi=> bi.Status != ItemStatus.CANCELLED.ToString())
                                             .Sum(a => a.NetPrice);
            bill.BillDate = DateTime.Now;
            bill.BillStatus = bill.BillStatus ?? BillStatus.INPROGRESS.ToString();
            if(isCreate)
                await _billRepo.AddAsync(bill);

            await _unitOfWork.CompleteAsync();  
            return bill;
        }

        public async Task<Bill> UpdateBillStatusAsync(Bill billToUpdate, string status)
        {
            if (billToUpdate == null)
               throw new Exception($"No Bill exists to update, with BillId: {billToUpdate.BillId}");
              
            billToUpdate.BillStatus = status;
            billToUpdate.BillItems.Where(bi=>bi.Status == ItemStatus.INPROGRESS.ToString())
                        .ToList().ForEach(item => item.Status = status);
             
            _billRepo.Update(billToUpdate);
            await _unitOfWork.CompleteAsync();

            return billToUpdate;
        }

        private readonly IBillRepository _billRepo;
        private readonly IProductRepository _prodRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
    }
}
