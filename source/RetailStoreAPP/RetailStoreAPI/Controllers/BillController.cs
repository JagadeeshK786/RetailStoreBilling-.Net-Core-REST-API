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

namespace RetailStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        public BillController(IBillService billService, IEmployeeService empService, IProductService productService, IMapper mapper, ILogger<BillController> logger)
        {
            _billService = billService;
            _empService = empService;
            _productService=productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BillModel>>> Get()
        {
            var bills = await _billService.GetBillsAsync();
  
            var billList = _mapper.Map<IEnumerable<Bill>, IEnumerable<BillModel>>(bills);
            _logger.LogInformation($"Returning {billList.Count()} bills.");

            return Ok(billList);
        }

        [HttpGet]
        [Route("{billId?}")]
        public async Task<ActionResult<BillModel>> GetBillByIdAsync(long billId)
        {
            var bill = await _billService.GetBillByIdAsync(billId);

            if (bill == null)
            {
                throw new KeyNotFoundException($"No Bill found with Bill Id: {billId}");
            }
            var billModel = _mapper.Map<Bill, BillModel>(bill);
            _logger.LogInformation($"Returning a Bill for Bill Id: {billId}.");

            return Ok(billModel);
        }

        [HttpGet]
        [Route("items/{billId?}")]
        public async Task<ActionResult<BillDetailsModel>> GetBillItemsByIdAsync(long billId)
        {
            var bill = await _billService.GetBillByIdAsync(billId);

            if (bill == null || !bill.BillItems.Any())
            {
                throw new KeyNotFoundException($"No Bill details found for BillId: {billId}");
            }
            var billModelDetailsVM = _mapper.Map<Bill, BillDetailsModel>(bill);
            _logger.LogInformation($"Returning Bill Item details for BillId: {billId}.");

            return Ok(billModelDetailsVM);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<BillModel>> CreateBill([FromBody] BillNewModel billModel)
        {
            //Validate Employee
            var empStatus = await _empService.IsValidEmployee(billModel.OperatorId);
            if (!empStatus)
                throw new AccessViolationException($"The operator '{billModel.OperatorId}' is not authorized to create a Bill.\n");
            
            var billAdded = await _billService.SaveAsync(_mapper.Map<BillNewModel, Bill>(billModel), true);
            var newBill = _mapper.Map<Bill, BillModel>(billAdded);

            _logger.LogInformation($"Created a Bill with BillId: {billAdded.BillId}.");

            return Ok(newBill);
        }

        [HttpPut]
        [Route("print/{billId?}")]
        public async Task<ActionResult<BillDetailsModel>> PrintBill(long billId)
        {
            var bill = await _billService.GetBillByIdAsync(billId);
                        
            if (bill == null || !bill.BillStatus.Equals(BillStatus.INPROGRESS.ToString()))
            {
                throw new KeyNotFoundException($"No bill is avaible for printing with BillId: {billId}");
            }

            var billPrinted = await _billService.UpdateBillStatusAsync(bill, BillStatus.READONLY.ToString());
            return _mapper.Map<Bill, BillDetailsModel>(billPrinted);
        }

        [HttpDelete]
        [Route("cancel/{billId?}")]
        public async Task<ActionResult<BillModel>> CancelBill(long billId)
        {
            var bill = await _billService.GetBillStatusByIdAsync(billId);

            if (bill == null || !bill.BillStatus.Equals(BillStatus.INPROGRESS.ToString()))
            {
                throw new KeyNotFoundException($"No bill is available for cancellation with BillId: {billId}");
            }

            var billCancelled = await _billService.UpdateBillStatusAsync(bill, BillStatus.CANCELLED.ToString());
            return _mapper.Map<Bill, BillModel>(billCancelled);
        }
        
        private readonly IBillService _billService;
        private readonly IProductService _productService;
        private readonly IEmployeeService _empService;
        private readonly IMapper _mapper;
        private readonly ILogger<BillController> _logger;
    }
}
