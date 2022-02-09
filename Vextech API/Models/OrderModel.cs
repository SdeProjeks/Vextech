namespace Vextech_API.Models
{
    public class OrderModel
    {
        public ulong ID { get; set; }
        public UserModel User { get; set; }
        public OrderStatusModel OrderStatus { get; set; }
        public string Address { get; set; }
        public string PostNumber { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProductModel> Products { get; set; }
    }
}
