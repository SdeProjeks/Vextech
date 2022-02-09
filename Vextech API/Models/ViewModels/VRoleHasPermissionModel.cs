namespace Vextech_API.Models.ViewModels
{
    public class VRoleHasPermissionModel
    {
        public ulong RoleID { get; set; }
        public string RoleName { get; set; }
        public ulong PermissionID { get; set; }
        public string PermissionName { get; set; }
    }
}
