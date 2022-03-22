using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VProductReviewModel
    {
        public ulong ID { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,20}")]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public decimal Price { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,20}")]
        public ulong UserID { get; set; }
        public ulong RoleID { get; set; }
        public string RoleName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ.,?''-'\s]{1,}")]
        public string comment { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-5]{1,1}")]
        public int Rating { get; set; }
    }
}
