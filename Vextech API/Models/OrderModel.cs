using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models
{
    public class OrderModel
    {
        public ulong ID { get; set; }
        public UserModel User { get; set; }
        public OrderStatusModel OrderStatus { get; set; }

        [RegularExpression(@"[a-zæøåA-ZÆØÅ0-9''-'\s]{5,255}")]
        public string Address { get; set; }

        [RegularExpression(@"[0-9]{4,5}")]
        public string PostNumber { get; set; }

        [RegularExpression(@"[a-zæøåA-ZÆØÅ0-9''-'\s]{3,255}")]
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProductModel> Products { get; set; }
    }
}
