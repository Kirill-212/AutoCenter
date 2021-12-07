using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer.ServiceCar
{
    public interface IAsyncHttpClientActionCar<T>: IAsynHttpClient<T>
    {
        Task<T> GetBySharePercentage(int sharePercentage);

        Task<int> DeleteAll(List<T> items);

        IAsyncHttpClientActionCar<T> SetJwt(string jwt = null);
    }
}
