namespace Vextech_API.Models
{
    public class ProductModel : IProductModel
    {
        public int ID { get; set; }
        public ProductBrandModel Brand { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public int Active { get; set; } = 0;
        public decimal Price { get; set; }
        public DateTime? Release_date { get; set; }
        public List<ProductCategoryNameModel> Categories { get; set; }
    }
}
