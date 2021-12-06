using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncServiceUser<T> : IAsyncService<T>
    {
        Task<IEnumerable<T>> GetAllUsersNotAddedToEmp();

        Task<T> GetByEmail(string email);
    }
}
