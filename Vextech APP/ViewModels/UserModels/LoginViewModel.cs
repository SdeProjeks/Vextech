using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels.UserModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        // hashed password
        [StringLength(256, ErrorMessage = "Password must be between 8 and 256", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-ZÆØÅ])(?=.*[a-zæøå])(?=.*\d)(?=.*\W).{8,40}",
        ErrorMessage = "Password must contain atleast 1 number, 1 upper and lowercase letter and 1 special character")]
        public string Password { get; set; }
    }
}
