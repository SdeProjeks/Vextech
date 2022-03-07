using System.ComponentModel.DataAnnotations.Schema;
using Vextech_API.DataAccess;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Models
{
    [Table("product_categoties")]
    public class ProductCategoriesModel
    {
        public int ProductID { get; }
        public int CategoryID { get; }
    }
}
