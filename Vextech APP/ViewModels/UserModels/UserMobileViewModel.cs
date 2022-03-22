using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels.UserModels
{
    public class UserMobileViewModel
    {
        public UserViewModel user { get; set; }
        public MobileCategoryViewModel mobileCategory { get; set; }
        [Required]
        [RegularExpression(@"^\+(?:[0-9]\x20?){4-16}[0-9]$")]
        public string PhoneNumber { get; set; }
    }
}
