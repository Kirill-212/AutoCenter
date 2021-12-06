using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.ContextDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Repository
{
    public class AsyncRepositoryCarEquipment : IAsyncRepositoryCarEquipment<CarEquipment>
    {
        private readonly ContextDb context;

        public AsyncRepositoryCarEquipment(ContextDb context)
        {
            this.context = context;
        }

        public async Task Create(CarEquipment item)
        {
            await context.CarEquipments.InsertOneAsync(item);
        }

        public async Task<IEnumerable<CarEquipment>> GetAll()
        {
            return await context.CarEquipments.Find(_ => true).ToListAsync();
        }

        public async Task<CarEquipment> GetById(string id)
        {
            return await context.CarEquipments.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<CarEquipment> GetByName(string name)
        {
            return await context.CarEquipments.Find(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task Remove(CarEquipment item)
        {
            await context.CarEquipments.DeleteOneAsync(new BsonDocument("_id", new ObjectId(item.Id)));
        }

        public async Task Update(CarEquipment item)
        {
            await context.CarEquipments.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(item.Id)), item);
        }
    }
}
