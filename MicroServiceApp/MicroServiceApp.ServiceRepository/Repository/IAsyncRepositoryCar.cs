using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryCar<T> : IAsyncRepository<T>
    {
        Task<T> GetByVin(string vin);

        Task UpdateRange(List<T> items);

        Task<IEnumerable<T>> GetWithoutClientCar();

        Task<T> GetByVinNotAddedEmp(string vin);

        Task<IEnumerable<T>> GetCarForUser();

        Task<IEnumerable<T>> GetCarByEmail(string email);
    }
}
