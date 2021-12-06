using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceCarEquipment : 
        IAsyncServiceCarEquipment<CarEquipment>
    {

        private readonly IAsyncRepositoryCarEquipment<CarEquipment> asyncRepository;

        public AsyncServiceCarEquipment(
            IAsyncRepositoryCarEquipment<CarEquipment> asyncRepository
            )
        {
            this.asyncRepository = asyncRepository;
        }

        public async Task Create(CarEquipment item)
        {
            await asyncRepository.Create(item);
        }

        public async Task<IEnumerable<CarEquipment>> GetAll()
        {
            return await asyncRepository.GetAll();
        }

        public async Task<CarEquipment> GetById(string id)
        {
            return await asyncRepository.GetById(id);
        }

        public async Task<CarEquipment> GetByName(string name)
        {
            return await asyncRepository.GetByName(name);
        }

        public async Task<CarEquipment> Remove(string name)
        {
            CarEquipment carEquipment = await asyncRepository.GetByName(name);
            if (carEquipment != null)
            {
                await asyncRepository.Remove(carEquipment);

                return carEquipment;

            }

            return null;
        }

        public async Task<CarEquipment> Update(CarEquipment item)
        {
            item.Id = (await GetByName(item.Name)).Id;
            await asyncRepository.Update(item);

            return item;
        }
    }
}
