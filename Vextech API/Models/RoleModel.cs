using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class RoleModel
    {
        public ulong ID { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,255}")]
        public string Name { get; set; }
    }
}
