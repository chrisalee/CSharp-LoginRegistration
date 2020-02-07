using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage="Enter your first name.")]
        [MinLength(3, ErrorMessage="First name must be 3 characters")]
        [MaxLength(20, ErrorMessage="First name must not be longer than 20 characters.")]
        [Display(Name="First Name: ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Enter your last name.")]
        [MinLength(3, ErrorMessage="Last name must be 3 characters")]
        [MaxLength(20, ErrorMessage="Last name must not be longer than 20 characters.")]
        [Display(Name="Last Name: ")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Enter your email.")]
        [EmailAddress]
        [Display(Name="Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage="Enter a password")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long")]
        [MaxLength(20, ErrorMessage="Password must not be longer than 20 characters.")]

        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage="Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Password and Confirm Password do not match.")]
        [Display(Name="Confirm Password: ")]
        public string ConfirmPassword { get; set; }

        // many to many 
        public List<Rsvp> GoingTo { get; set; }
        // one to many
        public List<Wedding> PlannedWed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }



    }
}