namespace Vextech_API.Models.ViewModels
{
    public class VProductModel 
    {
        public bool Active { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public string Descrption { get  ; set  ; }

        public int ID { get; }

        public string Name { get  ; set  ; }
        public decimal Price { get  ; set  ; }
        public DateTime Release_date { get  ; set  ; }
    }
}
