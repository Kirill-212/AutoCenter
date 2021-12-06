using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PostClientCarDto
    {
        [Required]
        public PostCarDto postCarDto { get; set; }

        [CheckFoundEmail]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [CheckRegisterNumber]
        [RegularExpression(
           @"^[0-9]{4} ['А','В','Е','І','К','М','Н','О','Р','С','Т','Х']{2}-[1-7]$",
           ErrorMessage = "RegisterNumber is not correct"
           )]
        [Required]
        public string RegisterNumber { get; set; }
    }
}
