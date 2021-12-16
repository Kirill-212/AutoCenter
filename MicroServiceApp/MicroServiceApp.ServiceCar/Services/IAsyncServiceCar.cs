using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceCar<T>
    {
        Task<int> Create(PostCarDto item, string jwt = null);

        Task<IEnumerable<T>> GetAll( string jwt = null);

        Task<T> GetById(int id, string jwt = null);

        Task<int> Update(PutCarDto item, string jwt = null);

        Task<int> Remove(string vin, string jwt = null);

        Task<T> GetByVin(string vin, string jwt = null);

        Task<IEnumerable<T>> GetWithoutClientCar( string jwt = null);

        Task<IEnumerable<T>> GetCarForUser( string jwt = null);

        Task<T> GetByVinNotAddedEmpValidAttr(string vin, string jwt = null);

        Task<IEnumerable<T>> GetCarByEmail(string email, string jwt = null);

        Task<int> UpdateStatus(string vin, string jwt = null);
    }
}
