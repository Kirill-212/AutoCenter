using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientNew<T>:IAsynHttpClient<T>
    {
        Task<T> GetByTitle(string title);
    }
}
