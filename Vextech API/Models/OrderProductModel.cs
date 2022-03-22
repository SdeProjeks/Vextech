using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class OrderProductModel
    {
        public ulong OrderID { get; set; }
        public ProductModel Product { get; set; }

        [RegularExpression(@"[0-9]{1,11}")]
        public int Amount { get; set; }
        [RegularExpression(@"^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$/")]
        public decimal Price { get; set; }
    }
}
