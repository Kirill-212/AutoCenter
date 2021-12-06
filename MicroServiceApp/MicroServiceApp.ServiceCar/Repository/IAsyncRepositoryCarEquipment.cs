using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Repository
{
   public interface IAsyncRepositoryCarEquipment<T>
    {
        Task Create(T item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByName(string name);

        Task<T> GetById(string id);

        Task Update(T item);

        Task Remove(T item);
    }
}
