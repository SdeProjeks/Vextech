namespace Vextech_API.Models
{
    public class UserSessionModel
    {
        public string ID { get; set; }
        public ulong UserID { get; set; }
        public DateTime Expires { get; set; }
    }
}
