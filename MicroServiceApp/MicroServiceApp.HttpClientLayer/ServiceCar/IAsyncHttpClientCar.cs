using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer.ServiceCar
{
    public interface IAsyncHttpClientCar<T>: IAsynHttpClient<T>
    {
        Task<T> GetByVin(string vin);

        Task<int> UpdateRange(List<T> items);
    }
}
