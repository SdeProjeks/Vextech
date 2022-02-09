namespace Vextech_API.Models.ViewModels
{
    public class VOrderModel
    {
        public ulong ID { get; set; }
        public string Address { get; set; }
        public string PostNumber { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatusID { get; set; }
        public string Name { get; set; }
        public ulong UserID { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
