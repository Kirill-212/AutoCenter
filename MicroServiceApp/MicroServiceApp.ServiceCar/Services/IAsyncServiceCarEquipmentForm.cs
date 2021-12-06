using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceCarEquipmentForm<T>
    {
        Task<T> Get();

        Task<bool> Update(T item);
    }
}
