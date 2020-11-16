using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using StoreDAC.Repositories;
using System.Threading.Tasks;
using StoreDAC.DBContext;
using StoreDAC.Entities;

namespace StoreServices.Repo
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(RetailStoreDBContext context) : base(context)
        { }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.Include(e=>e.Role).AsNoTracking().ToListAsync();
        }

        public async Task<Employee> FindByIdAsync(int id)
        {
            return await _context.Employees.Include(e=>e.Role).FirstOrDefaultAsync(e=>e.EmployeeId == id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> FindCountByIdAsync(int id)
        {
            return await _context.Employees.CountAsync(e=>e.EmployeeId == id);
        }
    }
}
