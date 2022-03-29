using Vextech_APP.ViewModels.UserModels;
namespace Vextech_APP.ViewModels.ProductModels
{
    public class ProductReviewViewModel
    {
        public ulong ID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public ProductViewModel Product { get; set; }
        public UserViewModel User { get; set; }
    }
}
