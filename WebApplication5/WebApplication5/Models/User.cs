namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    [Table("User")]
    public partial class User : IValidatableObject
    {
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The First Name is required")]
        [Column("First Name")]
        [StringLength(50)]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "You have input invalid charachters like ; : ! @ # $ % ^ * + ? / < > 1 2 3 4 5 6 7 8 9 0 ")]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The Last Name is required")]
        [Column("Last Name")]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "You have input invalid charachters like ; : ! @ # $ % ^ * + ?  / < > 1 2 3 4 5 6 7 8 9 0 ")]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [Display(Name = "Street Number")]
        [RegularExpression("^([0-9]+)$", ErrorMessage = "You have input an string. Please enter an integer value")]
        [Required(ErrorMessage = "The Street Number is required")]
        [Column("Street Number")]
        public int? Street_Number { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "The City is required")]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "You have input invalid charachters like ; : ! @ # $ % ^ * + ? / < > 1 2 3 4 5 6 7 8 9 0 ")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Province/State")]
        [Required(ErrorMessage = "The Province/State is required")]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "You have input invalid charachters like ; : ! @ # $ % ^ * + ? / < > 1 2 3 4 5 6 7 8 9 0 ")]
        [Column("Province/State")]
        [StringLength(50)]
        public string Province_State { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "The Country is required")]
        [StringLength(50)]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "The Postal Code is required")]
        [Column("Postal Code")]
        [StringLength(50)]

        public string Postal_Code { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!String.IsNullOrEmpty(Country)
               && !String.IsNullOrEmpty(Postal_Code))
            {
                String emailRegex = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)+$";
                Regex re = new Regex(emailRegex);
                switch (Country)
                {

                    case "Canada":
                        if (!Regex.IsMatch(Postal_Code, @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)+$"))
                            yield return new ValidationResult("Invalid Canada Postal Code.", new[] { "Postal_Code" });
                        break;
                    case "US":

                        if (!Regex.IsMatch(Postal_Code, @"^\d{5}(?:[-\s]\d{4})?$"))
                            yield return new ValidationResult("Invalid Canada Postal Code.", new[] { "Postal_Code" });
                        break;
                    default:
                        break;
                }
            }
        }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "The Phone Number is required")]
        [Column("Phone Number")]
        [StringLength(50)]
        [RegularExpression(@"^\D*([2-9]\d{2})(\D*)([2-9]\d{2})(\D*)(\d{4})\D*$", ErrorMessage = "Invalid phone number. Please enter the valid phone number")]
        public string Phone_Number { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Column("E-mail Address")]
        [StringLength(50)]
        public string E_mail_Address { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

    }
}