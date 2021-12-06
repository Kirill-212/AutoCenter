using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Repository;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceCarEquipmentForm :
        IAsyncServiceCarEquipmentForm<CarEquipmentForm>
    {
        private readonly IAsyncRepositoryCarEquipmentForm<CarEquipmentForm> asyncRepository;

        public AsyncServiceCarEquipmentForm(
            IAsyncRepositoryCarEquipmentForm<CarEquipmentForm> asyncRepository
            )
        {
            this.asyncRepository = asyncRepository;
        }

        public async Task<CarEquipmentForm> Get()
        {
            return await asyncRepository.Get();
        }

        public async Task<bool> Update(CarEquipmentForm item)
        {
            foreach (var i in item.EquipmentItems)
            {
                if (string.IsNullOrEmpty(i.Key))
                {
                    return false;
                }
                foreach (ValueCarEquipment j in item.EquipmentItems[i.Key])
                {
                    if (j.Cost < 0 || string.IsNullOrEmpty(j.Value))
                    {
                        return false;
                    }
                }
            }
            item.Id = (await Get()).Id;
            await asyncRepository.Update(item);

            return true;
        }
    }
}
