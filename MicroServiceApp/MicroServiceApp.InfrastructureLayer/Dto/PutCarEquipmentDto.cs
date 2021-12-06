using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutCarEquipmentDto
    {
        [CheckNameCarEquipmentForPut]
        [Required]
        [StringLength(50,
            MinimumLength = 3,
            ErrorMessage = "String length must be between 3 and 50 characters"
            )]
        public string Name { get; set; }

        [Required]
        public Dictionary<string, ValueCarEquipmentDto> Equipment { get; set; }

        [Required]
        public string UrlImg { get; set; }
    }
}
