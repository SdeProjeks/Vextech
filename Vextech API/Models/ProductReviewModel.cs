
namespace Vextech_API.Models
{
    public class ProductReviewModel
    {
        public ulong ID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public ProductModel Product { get; set; }
        public UserModel User { get; set; }

    }
}
