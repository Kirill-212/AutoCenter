using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class CarEquipmentFormDto
    {
        [Required]
        public Dictionary<string, ValueCarEquipmentDto[]> EquipmentItems { get; set; }
    }
}
