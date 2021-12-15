using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckVinForPostTetstDriveAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForCarService<Car> httpClientCar = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {

                if (httpClientCar.GetByVinValidAttr((string)value).Result == null)
                {
                    ErrorMessage = "This car is not valid";
                    return false;
                }
                var result = httpClientCar.GetByVinNotAddedEmpValidAttr((string)value).Result;
                if (result.ClientCar == null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "This car is not valid(client car)";
                }
            }

            return false;
        }
    }
}
