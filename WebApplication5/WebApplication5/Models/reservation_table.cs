namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class reservation_table : IValidatableObject
    {
        [Key]
        public int Reservation_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Check_in_date { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime Check_out_date { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            if (this.Check_out_date < this.Check_in_date)
            {
                ValidationResult mss = new ValidationResult("Expire date must be greater than or equal to Today");
                res.Add(mss);

            }
            return res;
        }
        [Required]
        [Range(1, 10)]
        public int No_of_rooms { get; set; }

        [Required]
        [Range(1, 10)]
        public int No_of_guests { get; set; }
    }
}
