using System.Threading.Tasks;

namespace StoreDAC.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}