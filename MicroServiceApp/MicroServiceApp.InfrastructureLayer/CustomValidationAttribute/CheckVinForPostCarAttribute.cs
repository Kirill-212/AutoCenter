using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckVinForPostCarAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForCarService<Car> httpClientCar = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClientCar.GetByVinValidAttr((string)value).Result;
                if (result == null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This VIN is already use";
                }
            }

            return false;
        }
    }
}

