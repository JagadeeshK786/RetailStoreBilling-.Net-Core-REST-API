using System.Threading.Tasks;
using StoreDAC;
using StoreDAC.DBContext;

namespace StoreDAC.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RetailStoreDBContext _context;

        public UnitOfWork(RetailStoreDBContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}