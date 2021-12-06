using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public interface IAsyncHttpClientCarEquipment<T>
    {
        Task<T> GetByName(string name);
    }
}
