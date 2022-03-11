using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VUserMobileModel
    {
        public ulong UserID { get; set; }
        [Required]
        [RegularExpression(@"^[0-9''-'\s]{1,12}")]
        public ulong MobileCategoryID { get; set; }
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\+(?:[0-9]\x20?){4-16}[0-9]$")]
        public string PhoneNumber { get; set; }
    }
}
