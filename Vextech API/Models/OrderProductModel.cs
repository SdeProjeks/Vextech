namespace Vextech_API.Models
{
    public class OrderProductModel
    {
        public OrderModel Order { get; set; }
        public ProductModel Product { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
