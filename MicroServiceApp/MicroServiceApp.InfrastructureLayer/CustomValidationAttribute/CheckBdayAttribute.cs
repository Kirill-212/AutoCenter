using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckBdayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
                if (dateTime < DateTime.Now && dateTime > DateTime.Now.AddYears(-18))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Date of realese can not valid";
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