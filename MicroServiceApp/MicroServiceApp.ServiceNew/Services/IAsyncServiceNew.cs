using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceNew.Services
{
    public interface IAsyncServiceNew<T>
    {
        Task<int> Create(NewWrapperDto<PostNewDto> item);

        Task<T> FindById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<int> Remove(string title);

        Task<int> Update(NewWrapperDto<PutNewDto> item);
    }
}
