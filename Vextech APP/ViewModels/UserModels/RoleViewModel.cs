using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels.UserModels
{
    public class RoleViewModel
    {
        public ulong ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,255}")]
        public string Name { get; set; }
    }
}
