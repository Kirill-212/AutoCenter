using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public interface IAsyncServiceEmployee<T>
    {
        Task<int> Create(PostEmployeeDto item);

        Task<T> FindById(int id);

        Task<T> FindByUserEmail(string email);

        Task<IEnumerable<T>> GetAll();

        Task<int> Remove(string email);

        Task<int> Update(PutEmployeeDto item);
    }
}
