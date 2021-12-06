using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutCarDto
    {
        [Required]
        [CheckNameCarEquipmentForPostOrPutCar]
        public string NameCarEquipment { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        [CheckVinForPutCarOrPostOrder]
        public string VIN { get; set; }

        [Required]
        public long CarMileage { get; set; }

        [CheckDateTimeForCar]
        [Required]
        public DateTime DateOfRealeseCar { get; set; }

        [CheckSharePercentageForPostOrPutCar]
        public int? SharePercentage { get; set; }
    }
}
