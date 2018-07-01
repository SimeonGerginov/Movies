using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Common.Validations
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;

            return date < DateTime.UtcNow;
        }
    }
}
