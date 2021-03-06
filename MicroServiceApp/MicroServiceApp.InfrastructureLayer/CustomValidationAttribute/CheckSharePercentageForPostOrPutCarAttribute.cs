using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckSharePercentageForPostOrPutCarAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null )
            {
                int result = Convert.ToInt32(value);
                if (result == 0) return true;
                if (result > 0 && result <= 100)
                {
                    return true;
                }
                ErrorMessage = "This SharePercentage is not valid value";
            }
            else
            {
                return true;

            }

            return false;
        }
    }
}
