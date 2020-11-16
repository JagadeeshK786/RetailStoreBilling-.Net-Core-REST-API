using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreServices;
using AutoMapper;
using StoreDAC.Entities;
using System.Threading.Tasks;
using RetailStoreAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace RetailStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarCodeController : ControllerBase
    {
        public BarCodeController(IBarCodeService codeService, IProductService prodService, IMapper mapper, ILogger<BarCodeController> logger)
        {
            _codeService = codeService;
            _prodService = prodService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BarcodeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BarcodeModel>>> Get()
        {
            var barcodes = await _codeService.GetBarCodesAsync();

            var barcodesList = _mapper.Map<IEnumerable<Barcode>, IEnumerable<BarcodeModel>>(barcodes);
            _logger.LogInformation($"Returning {barcodesList.Count()} barcode items.");

            return Ok(barcodesList);
        }

        [HttpGet]
        [Route("{code?}")]
        public async Task<ActionResult<ProductModel>> GetSerialNoByBarCode(string code)
        {
            var serialNo = await _codeService.GetSerialNoByBarCodeAsync(code);
            var product = await _prodService.GetProductByIdAsync(serialNo);
            _logger.LogInformation($"Returning '{product.ProductName}' for Barcode: {code}.");

            return _mapper.Map<Product, ProductModel>(product);         
        }

        private readonly IBarCodeService _codeService;
        IProductService _prodService;
        private readonly IMapper _mapper;
        private readonly ILogger<BarCodeController> _logger;
    }
}
