using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceCarEquipment<T>
    {
        Task Create(T item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByName(string name);

        Task<T> GetById(string id);

        Task<T> Update(T item);

        Task<T> Remove(string name);
    }
}
