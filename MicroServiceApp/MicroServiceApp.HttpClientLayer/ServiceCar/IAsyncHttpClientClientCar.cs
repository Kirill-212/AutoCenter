using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer.ServiceCar
{
    public interface IAsyncHttpClientClientCar<T> : IAsynHttpClient<T>
    {
        Task<T> GetByRegisterNumber(string registerNumber);
    }
}
