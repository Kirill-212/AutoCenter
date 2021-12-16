using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceNew.Services
{
    public interface IAsyncServiceNew<T>
    {
        Task<int> Create(NewWrapperDto<PostNewDto> item, string jwt = null);

        Task<T> FindById(int id, string jwt = null);

        Task<IEnumerable<T>> GetAll( string jwt = null);

        Task<int> Remove(string title, string jwt = null);

        Task<int> Update(NewWrapperDto<PutNewDto> item, string jwt = null);

        Task<T> GetByTitile(string title, string jwt = null);
    }
}
