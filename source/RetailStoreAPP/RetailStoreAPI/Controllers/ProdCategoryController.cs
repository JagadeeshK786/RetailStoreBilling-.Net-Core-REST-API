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
    [Route("api/prodgroup")]
    public class ProdCategoryController : ControllerBase
    {
        public ProdCategoryController(IProdCategoryService categoryService, IMapper mapper, ILogger<ProdCategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductCategoryModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductCategoryModel>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var catList = _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryModel>>(categories);

            _logger.LogInformation($"Returning {catList.Count()} Product categories.");

            return Ok(catList);
        }

        [HttpGet]
        [Route("products/{groupid?}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsByCategory(int groupid)
        {
            var category = await _categoryService.GetCategoryByIdAsync(groupid);

            if(category == null || !category.Products.Any())
            {
                throw new KeyNotFoundException($"No products found for category: {groupid}");
            }
            var prodList = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(category.Products);

            _logger.LogInformation($"Returning {prodList.Count()} Products for category: {groupid}.");
            return Ok(prodList);        
        }

        private readonly IProdCategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProdCategoryController> _logger;
    }
}
