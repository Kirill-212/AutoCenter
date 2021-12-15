using System;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.CustomValidationAttribute
{
    public class CheckTimeTestDriveAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string result = (string)value;
                if (result.IndexOf(':') == -1 || result.Length > 5||result.IndexOf(':')!=2)
                {
                    ErrorMessage = "Error input time -> 10:00";
                    return false;
                }
                string[] arr = result.Split(':');
                int hour = int.Parse(arr[0]);
                int minute = int.Parse(arr[1]);
                if (hour > 9 && hour < 17)
                {
                    if ( minute != 0)
                    {
                        ErrorMessage = "time test drive cannot valid";

                        return false;
                    }

                    return true;
                }
                else
                {
                    ErrorMessage = "time test drive cannot valid";

                    return false;
                }
            }
            ErrorMessage = "Date test drive cannot valid";

            return false;
        }
    }
}
