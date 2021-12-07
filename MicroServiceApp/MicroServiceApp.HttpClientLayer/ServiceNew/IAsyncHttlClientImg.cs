using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttlClientImg<T>
    {
        Task<int> RemoveRange(List<T> items);

        Task<int> AddRange(List<T> items);

        IAsyncHttlClientImg<T> SetJwt(string jwt = null);
    }
}
