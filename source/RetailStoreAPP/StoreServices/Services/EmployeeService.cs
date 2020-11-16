using StoreDAC;
using StoreDAC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreDAC.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using StoreServices.Infrastructure;

namespace StoreServices
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IEmployeeRepository employeeRepo, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _employeeRepo = employeeRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
                
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            //caching
            var employees = await _cache.GetOrCreateAsync(CacheKeys.EmployeesList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _employeeRepo.ListAsync();
            });
            
            //output validation
            if(employees == null || !employees.Any())
                throw new Exception($"No employees exist.");

            return employees;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _employeeRepo.FindByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException($"No Employee exists with Id: {id}");
            return employee;
        }

        public async Task<bool> IsValidEmployee(int id)
        {
            var empCount = await _employeeRepo.FindCountByIdAsync(id);
            return (empCount>0);
        }

        private readonly IEmployeeRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
    }
}
