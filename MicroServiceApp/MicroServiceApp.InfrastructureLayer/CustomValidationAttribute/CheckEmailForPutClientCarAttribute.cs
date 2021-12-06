using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckEmailForPutClientCarAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForUserService<User> httpClientUser = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientUser.GetByEmail((string)value).Result;
                if (result != null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This Email is cannot found";
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