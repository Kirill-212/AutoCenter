using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckRegisterNumberAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForCarService<ClientCar> httpClientClientCar = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientClientCar.GetByRegisterNumberValidAttr((string)value).Result;
                if (result == null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This Register Number is alredy use";
                }
            }

            return false;
        }
    }
}
