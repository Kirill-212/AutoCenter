using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutClientCarDto
    {
        [CheckFoundEmail]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [CheckEmailForPutClientCar]
        public string NewOwnerEmail { get; set; }

        [CheckFoundRegisterNumber]
        [RegularExpression(
           @"^[0-9]{4} ['А','В','Е','І','К','М','Н','О','Р','С','Т','Х']{2}-[1-7]$",
           ErrorMessage = "RegisterNumber is not correct"
           )]
        [Required]
        public string OldRegisterNumber { get; set; }

        [CheckRegisterNumberForPutClientCar]
        [RegularExpression(
           @"^[0-9]{4} ['А','В','Е','І','К','М','Н','О','Р','С','Т','Х']{2}-[1-7]$",
           ErrorMessage = "RegisterNumber is not correct"
           )]
        public string NewRegisterNumber { get; set; }
    }
}
