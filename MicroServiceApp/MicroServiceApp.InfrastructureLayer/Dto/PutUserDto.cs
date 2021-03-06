using MicroServiceApp.InfrastructureLayer.CustomValidationAttribute;
using MicroServiceApp.InfrastructureLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class PutUserDto
    {
        public Status Status { get; set; }

        public int Id { get; set; }

        public int RoleId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50,
            MinimumLength = 3,
            ErrorMessage = "String length must be between 3 and 50 characters"
            )]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "String length must be between 3 and 50 characters"
            )]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50,
            MinimumLength = 3,
            ErrorMessage = "String length must be between 3 and 50 characters"
            )]
        public string Surname { get; set; }

        [CheckBday]
        [Required(ErrorMessage = "Dbay is required")]
        public DateTime DBay { get; set; }

        [Required(ErrorMessage = "FPassword is required")]
        [RegularExpression(
            @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Password is not correct"
            )]
        public string Password { get; set; }

        [CheckFoundEmail]
        [RegularExpression(
            @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is not correct"
            )]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
