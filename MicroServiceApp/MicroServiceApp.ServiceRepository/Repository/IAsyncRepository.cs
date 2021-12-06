using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepository<T>
    {
        Task Create(T item);

        Task<T> FindById(int id);

        Task<IEnumerable<T>> Get();

        Task Remove(int id);

        Task Update(T item);
    }
}
