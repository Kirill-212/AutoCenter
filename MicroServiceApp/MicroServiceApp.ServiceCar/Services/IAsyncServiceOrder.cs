using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceOrder<T>
    {
        Task<int> Create(PostOrderDto item, string jwt = null);

        Task<IEnumerable<T>> GetAll( string jwt = null);

        Task<T> GetById(int id, string jwt = null);
    }
}
