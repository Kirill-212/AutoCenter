using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutEmployeeDto
    {

        [Required]
        [StringLength(50,
            MinimumLength = 10,
            ErrorMessage = "String length must be between 3 and 50 characters"
            )]
        public string Address { get; set; }

        [CheckFoundEmailForEmployee]
        [Required]
        [CheckFoundEmail]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

    }
}
