namespace Vextech_API.Models
{
    public class ProductCategoryNameModel : IProductCategoryNameModel
    {
        public int ID { get; }
        public int? Subcategory { get; set; }
        public string Category { get; set; }
    }
}
