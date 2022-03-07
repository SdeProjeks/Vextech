namespace Vextech_API.Models
{
    public class UserModel
    {
        public ulong ID { get; set; }
        public RoleModel Role { get; set; }
        public AddressModel Address { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string VatID { get; set; }
        public List<UserMobileModel> PhoneNumbers { get; set; }
        public string Session { get; set; }

    }
}
