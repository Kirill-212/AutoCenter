using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientUser<T>:IAsynHttpClient<T>
    {
        Task<IEnumerable<T>> GetAllUsersNotAddedToEmp();
    
        Task<T> GetByEmail(string email);
    }
}
