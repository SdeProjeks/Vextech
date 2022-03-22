using System.ComponentModel.DataAnnotations.Schema;

namespace Vextech_APP.ViewModels.ProductModels
{
    public class ProductCategoryNameModel
    {
        public int ID { get; set; }
        public List<ProductCategoryNameModel> subcategories { get; set; } = new();
        public string Category { get; set; }
    }
}
