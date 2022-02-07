namespace Vextech_API.Models
{
    public class ProductModel : IProductModel
    {
        public int ID { get; }
        public ProductBrandModel Brand { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public bool Active { get; set; } = false;
        public decimal Price { get; set; }
        public DateTime Release_date { get; set; }
    }
}
