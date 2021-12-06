using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.ContextDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Repository
{
    public class AsyncRepositoryCarEquipmentForm : IAsyncRepositoryCarEquipmentForm<CarEquipmentForm>
    {
        private readonly ContextDb context;

        public AsyncRepositoryCarEquipmentForm(ContextDb context)
        {
            this.context = context;
        }

        public async Task<CarEquipmentForm> Get()
        {
            return await context.CarEquipmentForms.Find(new BsonDocument("_id", new ObjectId(context.IdForm))).FirstOrDefaultAsync();
        }

        public async Task Update(CarEquipmentForm item)
        {
           await context.CarEquipmentForms.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(item.Id)), item);
        }
    }
}
