using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryClientCar<T> : IAsyncRepository<T>
    {
        Task<T> GetByRegisterNumber(string registerNumber);
    }
}
