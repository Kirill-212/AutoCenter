using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryCar<T> : IAsyncRepository<T>
    {
        Task<T> GetByVin(string vin);

        Task UpdateRange(List<T> items);
    }
}
