using System.ComponentModel.DataAnnotations.Schema;

namespace Vextech_API.Models
{
    [Table("product_Brand")]
    public class ProductBrandModel : IProductBrandModel
    {
        public int ID { get; set; }
        public string Brand { get; set; }
    }
}
