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
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeService empService, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _empService = empService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> Get()
        {
            var employees = await _empService.GetEmployeesAsync();

            var employeesList = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(employees);
            _logger.LogInformation($"Returning {employeesList.Count()} employee items.");

            return Ok(employeesList);
        }

        [HttpGet]
        [Route("{eid?}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeById(int eid)
        {
            var employee = await _empService.GetByIdAsync(eid);

            if (employee == null)
            {
                throw new KeyNotFoundException($"No Employee found with EmpId: {eid}");
            }
            var employeeModel = _mapper.Map<Employee, EmployeeModel>(employee);
            _logger.LogInformation($"Returning '{employeeModel.FirstName} - {employeeModel.LastName}' for Id: {eid}.");
            return employeeModel;           
        }

        [HttpGet]
        [Route("valid/{eid?}")]
        public async Task<ActionResult<bool>> IsValidEmplyee(int eid)
        {
            var status = await _empService.IsValidEmployee(eid);
            return status;
        }
        
        private readonly IEmployeeService _empService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
    }
}
