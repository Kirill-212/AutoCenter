using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class CarEquipmentForm
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Dictionary<string, ValueCarEquipment[]> EquipmentItems { get; set; }
    }
}
