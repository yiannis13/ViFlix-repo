using System;
using System.ComponentModel.DataAnnotations;
using Common.Models.Domain;

namespace Common.Validation
{
    public class ValidateAgeIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == (byte)MembershipType.Type.Unknown
                || customer.MembershipTypeId == (byte)MembershipType.Type.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (!customer.Birthday.HasValue)
            {
                return new ValidationResult("Birthday is required when you have membership.");
            }

            var age = CalculateAge(customer.Birthday.Value);
            if (age < 18)
            {
                return new ValidationResult("You must be at least 18 year's old to have membership.");
            }

            return ValidationResult.Success;
        }

        private static int CalculateAge(DateTime birthday)
        {
            var now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var dateOfBirth = int.Parse(birthday.ToString("yyyyMMdd"));

            return (now - dateOfBirth) / 10000;
        }
    }
}