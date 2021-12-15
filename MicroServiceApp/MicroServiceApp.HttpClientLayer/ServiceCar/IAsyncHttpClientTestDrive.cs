using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer.ServiceCar
{
    public interface IAsyncHttpClientTestDrive<T> : IAsynHttpClient<T>
    {
        Task<int> DeleteAll(List<T> items);

        IAsyncHttpClientTestDrive<T> SetJwt(string jwt = null);

        Task<IEnumerable<T>> GetByVin(string vin);

        Task<IEnumerable<T>> GetByVinAttr(string vin);

        Task<T> GetByAllData(T item);
    }
}