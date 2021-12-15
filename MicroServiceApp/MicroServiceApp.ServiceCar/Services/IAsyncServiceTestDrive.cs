using MicroServiceApp.InfrastructureLayer.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public interface IAsyncServiceTestDrive<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<int> Create(PostTestDriveDto item);

        Task<int> Put(PutTestDriveDto item);
    }
}
