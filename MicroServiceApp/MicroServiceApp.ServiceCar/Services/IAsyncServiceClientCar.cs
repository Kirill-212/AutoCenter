using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceClientCar<T>
    {
        Task<int> Create(PostClientCarDto item, string jwt = null);

        Task<IEnumerable<T>> GetAll( string jwt = null);

        Task<T> GetById(int id, string jwt = null);

        Task<int> Update(PutClientCarDto item, string jwt = null);

        Task<int> Remove(string registerNumber, string jwt = null);
    }
}
