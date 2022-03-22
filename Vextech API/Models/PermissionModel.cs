using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class PermissionModel
    {
        [Required]
        public ulong ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ''-'\s]")]
        public string Name { get; set; }
    }
}
