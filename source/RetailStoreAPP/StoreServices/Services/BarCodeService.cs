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
    public class BarCodeService : IBarCodeService
    {
        public BarCodeService(IBarcodeRepository barcodeRepo, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _barcodeRepo = barcodeRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Barcode>> GetBarCodesAsync()
        {
            //caching
            var barCodes = await _cache.GetOrCreateAsync(CacheKeys.BarCodeList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _barcodeRepo.ListAsync();
            });

            //output validation
            if (barCodes == null || !barCodes.Any())
                throw new Exception($"No bar codes exist.");

            return barCodes;
        }

        public async Task<long> GetSerialNoByBarCodeAsync(string barCode)
        {
            var model = await _barcodeRepo.FindByIdAsync(barCode);

            if (model == null)
                throw new Exception($"No Serial.No exists with Bar code: {barCode}");

            return model.SerialNumber;
        }

        private readonly IBarcodeRepository _barcodeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

    }
}
