using System.ComponentModel.DataAnnotations.Schema;

namespace Vextech_APP.ViewModels
{
    public class ProductCategoryNameModel
    {
        public int ID { get; set; }
        public int? Subcategory { get; set; }
        public string Category { get; set; }
    }
}
