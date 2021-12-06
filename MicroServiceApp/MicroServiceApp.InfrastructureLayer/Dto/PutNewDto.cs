using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutNewDto
    {
        [CheckTitleForPutNew]
        [StringLength(10,
            MinimumLength = 3,
            ErrorMessage = "Title length must be between 3 and 10 characters"
            )]
        [Required]
        public string Title { get; set; }

        [StringLength(50,
            MinimumLength = 13,
            ErrorMessage = "Description length must be between 13 and 50 characters"
            )]
        [Required]
        public string Description { get; set; }

        [CheckFoundEmailForEmployeeForNew]
        [Required]
        public string Email { get; set; }

    }
}
