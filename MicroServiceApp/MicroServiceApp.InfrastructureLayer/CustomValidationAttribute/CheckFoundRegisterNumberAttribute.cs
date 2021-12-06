using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckFoundRegisterNumberAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForCarService<ClientCar> httpClientClientCar = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientClientCar.GetByRegisterNumber((string)value).Result;
                if (result != null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This Register Number not found";
                }
            }

            return false;
        }
    }
}