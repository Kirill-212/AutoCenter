using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncService<T>
    {
        Task<int> Create(T item, string jwt = null);

        Task<T> FindById(int id, string jwt = null);

        Task<IEnumerable<T>> GetAll(string jwt = null);

        Task<int> Remove(string email, string jwt = null);

        Task<int> Update(T item, string jwt = null);
    }
}
