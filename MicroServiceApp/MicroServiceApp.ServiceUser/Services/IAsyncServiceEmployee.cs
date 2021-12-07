using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncServiceEmployee<T>
    {
        Task<int> Create(PostEmployeeDto item, string jwt = null);

        Task<T> FindById(int id, string jwt = null);

        Task<T> FindByUserEmail(string email, string jwt = null);

        Task<IEnumerable<T>> GetAll(string jwt = null);

        Task<int> Remove(string email, string jwt = null);

        Task<int> Update(PutEmployeeDto item, string jwt = null);
    }
}
