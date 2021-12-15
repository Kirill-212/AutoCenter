using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceCar<T>
    {
        Task<int> Create(PostCarDto item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<int> Update(PutCarDto item);

        Task<int> Remove(string vin);

        Task<T> GetByVin(string vin);

        Task<IEnumerable<T>> GetWithoutClientCar();

        Task<IEnumerable<T>> GetCarForUser();

        Task<T> GetByVinNotAddedEmpValidAttr(string vin);

        Task<IEnumerable<T>> GetCarByEmail(string email);
    }
}
