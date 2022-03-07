namespace Vextech_API.Models.ViewModels
{
    public class VProductReviewModel
    {
        public ulong ID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public decimal Price { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public ulong UserID { get; set; }
        public ulong RoleID { get; set; }
        public string RoleName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string comment { get; set; }
        public int Rating { get; set; }
    }
}
