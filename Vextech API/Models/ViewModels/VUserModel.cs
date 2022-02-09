namespace Vextech_API.Models.ViewModels
{
    public class VUserModel
    {
        public ulong ID { get; set; }
        public ulong RoleID { get; set; }
        public string Name { get; set; }
        public ulong PermissionsID { get; set; }
        public string PermissionName { get; set; }
        public int AddressID { get; set; }
        public string Address { get; set; }
        public int PostNumberID { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string VatID { get; set; }
    }
}
