using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceClientCar<T>
    {
        Task<int> Create(PostClientCarDto item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<int> Update(PutClientCarDto item);

        Task<int> Remove(string registerNumber);
    }
}
