using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VOrderProductModel
    {
        [Required]
        [RegularExpression(@"[0-9]{1,11}")]
        public int Amount { get; set; }

        public decimal Price { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
    }
}
