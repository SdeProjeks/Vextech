using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Username to short")]
        public string username { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage = "Message to long")]
        public string message { get; set; }
    }
}
