using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncServiceUser<T> : IAsyncService<T>
    {
        Task<IEnumerable<T>> GetAllUsersNotAddedToEmp(string jwt=null);

        Task<T> GetByEmail(string email, string jwt = null);

        Task<int> UpdateStatusByEmail(string email, string jwt = null);
    }
}
