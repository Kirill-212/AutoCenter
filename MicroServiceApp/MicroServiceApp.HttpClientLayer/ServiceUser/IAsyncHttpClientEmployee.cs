using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientEmployee<T> : IAsynHttpClient<T>
    {
        Task<T> GetByUserId(int id);

        Task<T> GetByUserEmail(string email);
    }
}
