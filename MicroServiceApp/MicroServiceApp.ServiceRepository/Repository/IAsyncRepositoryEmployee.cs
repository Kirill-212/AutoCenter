using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryEmployee<T> : IAsyncRepository<T>
    {
        Task<T> FindByIdUser(int id);

        Task<T> FindByUserEmail(string email);
    }
}
