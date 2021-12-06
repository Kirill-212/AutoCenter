using MicroServiceApp.InfrastructureLayer.Models;
using MongoDB.Driver;

namespace MicroServiceApp.ServiceCar.ContextDB
{
    public class ContextDb
    {
        public readonly string IdForm;
        public readonly IMongoCollection<CarEquipment> CarEquipments; 
        public readonly IMongoCollection<CarEquipmentForm> CarEquipmentForms;
        public ContextDb(string connectionString, string idForm)
        {
            IdForm = idForm;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            CarEquipments = database.GetCollection<CarEquipment>("CarEquipment");
            CarEquipmentForms = database.GetCollection<CarEquipmentForm>("CarEquipmentForm");
        }
    }
}
