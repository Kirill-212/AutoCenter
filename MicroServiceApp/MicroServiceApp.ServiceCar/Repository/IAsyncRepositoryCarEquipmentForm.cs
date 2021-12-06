using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Repository
{
    public interface IAsyncRepositoryCarEquipmentForm<T>
    {
        public Task<T> Get();

        public Task Update(T item);
    }
}
