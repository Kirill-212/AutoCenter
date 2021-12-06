using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryImg<T> : IAsyncRepository<T>
    {
        Task DeleteRange(List<T> items);

        Task AddRange(List<T> items);
    }
}
