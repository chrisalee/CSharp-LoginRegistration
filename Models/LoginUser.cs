
using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models
{
    public class LoginUser
    {
    // this does not get added to our database, it is for validation
        [Required(ErrorMessage="Email required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Invalid Email/Password Model")]
        [Display(Name="Email: ")]
        public string LoginEmail { get; set; }
        [Required(ErrorMessage="Password required")]
        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
    // named different from user model, different names would need to be used for same page forms
        public string LoginPassword { get; set; }
    }
}