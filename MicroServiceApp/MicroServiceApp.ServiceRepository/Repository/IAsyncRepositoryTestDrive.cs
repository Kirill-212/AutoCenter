using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryTestDrive<T> : IAsyncRepository<T>
    {
        Task DeleteRange(List<T> items);

        Task<IEnumerable<T>> GetByVin(string vin);

        Task<T> GetByAllData(T item);
    }
}
