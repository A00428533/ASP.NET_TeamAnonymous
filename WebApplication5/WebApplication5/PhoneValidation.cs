using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication5
{
    
    class PhoneValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
                            ValidationContext validationContext)
        {

            string str = value.ToString();

            if (!IsUSorCanadianPhoneNumber(str))
            {
                return new ValidationResult("Invalid phone number");
            }

            return ValidationResult.Success;
        }

        private bool IsUSorCanadianPhoneNumber(string phoneNumber)
        {
            bool IsUSorCanadianPhoneNumber = false;
            string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            Regex regex = new Regex(pattern);
            return IsUSorCanadianPhoneNumber = regex.IsMatch(phoneNumber);
        }

    }
}