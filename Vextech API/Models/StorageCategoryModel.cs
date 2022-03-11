using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class StorageCategoryModel
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ''-'\s]{1,255}")]
        public string Category { get; set; }
    }
}
