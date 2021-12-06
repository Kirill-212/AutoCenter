using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryRole<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
