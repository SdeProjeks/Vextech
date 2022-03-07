using System.ComponentModel.DataAnnotations.Schema;

namespace Vextech_API.Models
{
    [Table("product_category_names")]
    public class ProductCategoryNameModel : IProductCategoryNameModel
    {
        public int ID { get; set; }
        public int? Subcategory { get; set; }
        public string Category { get; set; }
    }
}
