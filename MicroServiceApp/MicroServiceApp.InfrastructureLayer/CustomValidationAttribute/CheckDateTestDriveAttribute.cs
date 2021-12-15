using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckDateTestDriveAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
                if (dateTime > DateTime.Now && dateTime < DateTime.Now.AddYears(1))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Date test drive cannot valid";

                    return false;
                }
            }
            ErrorMessage = "Date test drive cannot valid";

            return false;
        }
    }
}