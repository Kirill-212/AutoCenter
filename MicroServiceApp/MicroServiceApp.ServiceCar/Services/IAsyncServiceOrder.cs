using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceOrder<T>
    {
        Task<int> Create(PostOrderDto item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);
    }
}
