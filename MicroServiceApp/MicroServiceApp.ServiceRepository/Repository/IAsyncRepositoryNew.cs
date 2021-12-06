using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryNew<T> : IAsyncRepository<T>
    {
        Task<T> GetByTitle(string title);
    }
}
