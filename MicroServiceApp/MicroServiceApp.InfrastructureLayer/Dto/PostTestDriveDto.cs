using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PostTestDriveDto
    {

        [CheckVinForPutCarOrPostOrder]
        [CheckVinForPostTetstDrive]
        [Required]
        public string Vin { get; set; }

        [CheckTimeTestDrive]
        [Required]
        public string Time { get; set; }

        [CheckDateTestDrive]
        [Required]
        public DateTime Date { get; set; }
    }
}
