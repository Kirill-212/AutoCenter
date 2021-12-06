using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public interface IAsyncRepositoryUser<T> : IAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAllUsersNotAddedToEmp();

        Task<T> GetByEmail(string email);
    }
}
