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
    public class ProductController : ControllerBase
    {
        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var products = await _productService.GetProductsAsync();
  
            var prodList = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
            _logger.LogInformation($"Returning {prodList.Count()} Category items.");

            return Ok(prodList);
        }

        [HttpGet]
        [Route("{serialno?}")]
        public async Task<ActionResult<ProductModel>> GetProductBySno(long serialno)
        {
            var product = await _productService.GetProductByIdAsync(serialno);

            if (product == null)
            {
                throw new KeyNotFoundException($"No product found with Serial no: {serialno}");
            }
            var productModel = _mapper.Map<Product, ProductModel>(product);
            _logger.LogInformation($"Returning '{productModel.ProductName}' for Serial No: {serialno}.");

            return Ok(productModel);           
        }


        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
    }
}
