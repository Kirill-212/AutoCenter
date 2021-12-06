using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncService<T>
    {
        Task<int> Create(T item);

        Task<T> FindById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<int> Remove(string email);

        Task<int> Update(T item);
    }
}
