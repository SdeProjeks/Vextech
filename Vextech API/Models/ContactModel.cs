using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class ContactModel
    {
        public ulong ID { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "Shorten your name")]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ''-'\s]", ErrorMessage = "can only contain lowercase and uppercate letters")]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "your email is to long")]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ0-9!&''-'\s]",ErrorMessage = "Can only contain some special charactors")]
        public string Message { get; set; }
    }
}
