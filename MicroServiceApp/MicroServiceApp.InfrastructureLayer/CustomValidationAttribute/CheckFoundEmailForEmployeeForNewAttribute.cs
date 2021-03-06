using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckFoundEmailForEmployeeForNewAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForUserService<Employee> httpClientUser = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientUser.GetByUserEmailValidAttr((string)value).Result;
                if (result != null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This emp not found";
                    return false;
                }
            }
            return false;

        }
    }
}