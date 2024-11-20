using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Test1.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&anp;*()_+].*$",
            ErrorMessage ="password must contains 1 Uppercase, 1 Lowercase ,1 Digit ,1 Special Character")]
        public string Password { get; set; }

    }
}
