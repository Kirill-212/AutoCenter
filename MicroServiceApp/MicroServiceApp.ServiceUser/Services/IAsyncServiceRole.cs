using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncServiceRole<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
