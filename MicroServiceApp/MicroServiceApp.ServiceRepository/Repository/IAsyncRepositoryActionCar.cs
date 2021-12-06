using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryActionCar<T> : IAsyncRepository<T>
    {
        Task<T> GetBySharePercentage(int sharePercentage);

        Task DeleteRange(List<T> items);
    }
}
