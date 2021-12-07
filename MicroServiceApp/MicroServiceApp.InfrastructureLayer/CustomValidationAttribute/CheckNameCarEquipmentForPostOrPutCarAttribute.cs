using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckNameCarEquipmentForPostOrPutCarAttribute : ValidationAttribute
    {
        private readonly AsyncHttpClientForCarService<CarEquipment> httpClient = new();

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var result = httpClient.GetByNameValidAttr((string)value).Result;
                if (result != null)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Check name car equipment";
                }
            }

            return false;
        }
    }
}

