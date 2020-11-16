using System.Threading.Tasks;
using StoreDAC;
using StoreDAC.DBContext;

namespace StoreDAC.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly RetailStoreDBContext _context;

        public BaseRepository(RetailStoreDBContext context)
        {
            _context = context;
        }
    }
}