using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MicroServiceApp.InfrastructureLayer.Models
{
    public class CarEquipment
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, ValueCarEquipment> Equipment { get; set; }

        public string UrlImg { get; set; } 
    }
}
