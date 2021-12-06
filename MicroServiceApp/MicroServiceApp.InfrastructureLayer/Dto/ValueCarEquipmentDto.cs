using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class ValueCarEquipmentDto
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
