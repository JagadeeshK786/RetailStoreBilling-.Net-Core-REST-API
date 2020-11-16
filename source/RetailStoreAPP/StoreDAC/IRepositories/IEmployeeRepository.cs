using System;
using System.Collections.Generic;
using System.Text;
using StoreDAC.Entities;
using System.Threading.Tasks;

namespace StoreDAC.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> FindByIdAsync(int empId);
        Task<int> FindCountByIdAsync(int empId);
    }
}
