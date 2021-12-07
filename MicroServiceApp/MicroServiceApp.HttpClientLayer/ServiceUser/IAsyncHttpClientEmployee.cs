using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientEmployee<T> : IAsynHttpClient<T>
    {
        Task<T> GetByUserId(int id);

        Task<T> GetByUserEmail(string email);

        Task<T> GetByUserEmailValidAttr(string email);

        IAsyncHttpClientEmployee<T> SetJwt(string jwt = null);
    }
}
