using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
// ////////////////////////////////////////////////////
        [Required(ErrorMessage = "Enter name of Wedder One")]
        [MinLength(2,ErrorMessage = "The name need to be at least 2 characters")]
        public string WedderOne { get; set; }
// ///////////////////////////////////////////////////
        [Required(ErrorMessage = "Enter name of Wedder Two")]
        [MinLength(2,ErrorMessage = "The name need to be at least 2 characters")]
        public string WedderTwo { get; set; }
// ////////////////////////////////////////////////////
        [Required(ErrorMessage = "Enter an address")]

        public string Address { get; set; }
// ///////////////////////////////////////////////////
        [Required(ErrorMessage = "Enter the date")]
        [FutureDate]
        public DateTime Date { get; set; }
// ///////////////////////////////////////////////////////
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
// ///////////////////////////////////////////////////////

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime check;
            if( value is DateTime)
            {
                check = (DateTime)value;
                if(check < DateTime.Now)
                {
                    return new ValidationResult("Enter a future date for the wedding");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("Enter a date in the future");
            }
        }
    }
}