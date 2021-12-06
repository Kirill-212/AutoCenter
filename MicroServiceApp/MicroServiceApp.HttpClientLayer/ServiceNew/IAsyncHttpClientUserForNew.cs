using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientUserForNew<T>
    {
        Task<T> GetUserByEmail(string email);
    }
}
