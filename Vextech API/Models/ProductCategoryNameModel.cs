namespace Vextech_API.Models
{
    public class ProductCategoryNameModel : IProductCategoryNameModel
    {
        public int ID { get; set; }
        public int? Subcategory { get; set; }
        public string Category { get; set; }
    }
}
