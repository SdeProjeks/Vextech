using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class UserMobileModel
    {
        public UserModel user { get; set; }
        public MobileCategoryModel mobileCategory { get; set; }
        [Required]
        [RegularExpression(@"^\+(?:[0-9]\x20?){4-16}[0-9]$")]
        public string PhoneNumber { get; set; }
    }
}
