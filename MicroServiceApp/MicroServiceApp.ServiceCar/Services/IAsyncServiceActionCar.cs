using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceActionCar<T>
    {
        Task<int> DeleteAll( string jwt = null);
    }
}
