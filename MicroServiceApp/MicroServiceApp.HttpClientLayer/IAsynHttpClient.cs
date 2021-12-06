using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsynHttpClient<T>
    {
        Task<T> GetById(int id);

        Task<int> Add(T entity);

        Task<int> Update(T entity);

        Task<int> Remove(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
