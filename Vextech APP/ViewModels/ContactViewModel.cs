using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Shorten your name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "your email is to long")]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public string Success { get; set; } = "";
    }
}
