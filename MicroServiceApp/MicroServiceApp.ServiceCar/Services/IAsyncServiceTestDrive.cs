using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceTestDrive<T>
    {
        Task<IEnumerable<T>> GetAll( string jwt = null);

        Task<int> Create(PostTestDriveDto item, string jwt = null);

        Task<int> Put(PutTestDriveDto item, string jwt = null);
    }
}
