using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientRole<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
