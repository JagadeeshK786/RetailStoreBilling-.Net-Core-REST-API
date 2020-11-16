using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreDAC.Entities;

namespace StoreServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetByIdAsync(int empId);
        Task<bool> IsValidEmployee(int empId);
    }
}
