using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckFoundEmailForEmployeeAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForUserService<Employee> httpClientUser = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientUser.GetByUserEmail((string)value).Result;
                if (result != null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This user not found on the emp";
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}