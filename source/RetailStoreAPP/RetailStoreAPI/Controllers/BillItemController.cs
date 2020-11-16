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
using StoreServices.Infrastructure;
using RetailStoreAPI.Filters;
using StoreServices.ServiceModels;

namespace RetailStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillItemController : ControllerBase
    {
        public BillItemController(IBillItemService billItemService, IBillService billService, IEmployeeService empService, IMapper mapper, ILogger<BillItemController> logger)
        {
            _billItemService = billItemService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillItemModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BillItemModel>>> Get()
        {
            var items = await _billItemService.GetBillItemsAsync();

            var itemsList = _mapper.Map<IEnumerable<BillItem>, IEnumerable<BillItemModel>>(items);
            _logger.LogInformation($"Returning {itemsList.Count()} bills.");

            return Ok(itemsList);
        }

        [HttpGet]
        [Route("{itemid?}")]
        public async Task<ActionResult<BillItemModel>> GetBillByIdAsync(long itemid)
        {
            var item = await _billItemService.GetBillItemByIdAsync(itemid);

            if (item == null)
            {
                throw new KeyNotFoundException($"No Bill Item found with Serial no: {itemid}");
            }
            var itemModel = _mapper.Map<BillItem, BillItemModel>(item);
            _logger.LogInformation($"Returning a Bill Item for Bill No: {itemid}.");

            return Ok(itemModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<BillItemUpdateModel>> UpdateBillItems([FromBody] BillItemUpdateModel billItemModel)
        {
            await _billItemService.UpdateAsync(_mapper.Map<BillItemUpdateModel, BillItemUpdateSM>(billItemModel));
            return Ok(billItemModel);
        }

        [HttpDelete]
        [Route("cancel/{itemid?}")]        
        public async Task<ActionResult<BillItemModel>> CancelBillItems(long itemid)
        {
            if(itemid<=0)
                throw new KeyNotFoundException($"No Bill Item is available for cancellation with Id: {itemid}");

            var billItem = await _billItemService.GetBillItemByIdAsync(itemid);

            if (billItem == null || !(billItem.Status == BillStatus.INPROGRESS.ToString()))
                throw new KeyNotFoundException($"No Bill Item is available for cancellation with Id: {itemid}");

            var billCancelled = await _billItemService.CancelAsync(billItem);
            return _mapper.Map<BillItem, BillItemModel>(billCancelled);
        }

        private readonly IBillItemService _billItemService;
        private readonly IMapper _mapper;
        private readonly ILogger<BillItemController> _logger;
    }
}
