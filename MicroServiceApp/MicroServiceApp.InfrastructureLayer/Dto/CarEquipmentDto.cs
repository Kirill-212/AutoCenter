using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class CarEquipmentDto
    {

        public string Name { get; set; }

        public Dictionary<string, ValueCarEquipment> Equipment { get; set; }

        public string UrlImg { get; set; }
    }
}
