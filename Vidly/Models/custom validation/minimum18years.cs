using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models.custom_validation
{
    public class minimum18years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.PayAsYouGo ||
                customer.MembershipTypeId == MembershipType.unKnown)
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            var customerAge = DateTime.Now.Year - customer.Birthdate.Value.Year;

            return customerAge >= 18
                ? ValidationResult.Success
                : new ValidationResult("You should be 18 years at least to go on with this membership");
        }
    }
}