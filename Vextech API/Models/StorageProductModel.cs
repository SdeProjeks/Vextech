namespace Vextech_API.Models
{
    public class StorageProductModel
    {
        public StorageModel Storage { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
