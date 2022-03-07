using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<RoleModel>> GetRoles()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = "SELECT * FROM roles;";

                var result = SqlDataAccess.LoadData<RoleModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There are no roles");
                }

                return result;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<RoleModel>> GetRoleByID(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT * FROM roles WHERE ID = {id};";

                var result = SqlDataAccess.LoadData<RoleModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "this is not the role you are looking for");
                }

                return result;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult CreateRole(string name)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                RoleModel data = new RoleModel()
                {
                    Name = name
                };
                string sql;
                sql = @"INSERT INTO roles (name) VALUES (@Name);";
                
                var result = SqlDataAccess.SaveData<RoleModel>(sql,data);
                return Ok("Role created succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "Role was not created, because of invalid data");
            }
        }

        [HttpPut]
        public ActionResult UpdateRoleByName(ulong id, string name) 
        {
            try 
	        {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                RoleModel data = new RoleModel()
                {
                    ID = id,
                    Name = name
                };
                string sql;
                sql = $"UPDATE roles SET Name = '{name}' WHERE ID = {id};";

                var result = SqlDataAccess.UpdateData(sql);

                return Ok("Role updated succesfully");
	        }
	        catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "Role Update failed, because the role was not found");
            }
        }



        public List<RoleHasPermissionModel> result = new List<RoleHasPermissionModel>();


        [HttpGet]
        public ActionResult<List<RoleHasPermissionModel>> GetRoleHasPermissions()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = "SELECT role_has_permission.RoleID,roles.Name AS RoleName,role_has_permission.PermissionID,permissions.Name AS PermissionName FROM role_has_permission INNER JOIN roles ON role_has_permission.RoleID = roles.ID INNER JOIN permissions ON role_has_permission.PermissionID = permissions.ID;";
                
                var DatabaseResult = SqlDataAccess.LoadData<VRoleHasPermissionModel>(sql).ToArray<VRoleHasPermissionModel>();

                foreach (var RolePermissions in DatabaseResult)
	            {
                    RoleHasPermissionModel remaping = new RoleHasPermissionModel()
                    {
                        Role = new RoleModel()
                        {
                            ID = RolePermissions.RoleID,
                            Name = RolePermissions.RoleName,
                        },
                        Permission = new PermissionModel()
                        {
                            ID = RolePermissions.PermissionID,
                            Name = RolePermissions.PermissionName,
                        }
                        
                    };
                    result.Add(remaping);
	            }
                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There are no roles that has a permission");
                }
                return result;
	        }
	        catch (Exception ex)
	        {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
	        }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<RoleHasPermissionModel>> GetOneRolesPermissions(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT role_has_permission.RoleID,roles.Name AS RoleName,role_has_permission.PermissionID,permissions.Name AS PermissionName FROM role_has_permission INNER JOIN roles ON role_has_permission.RoleID = roles.ID INNER JOIN permissions ON role_has_permission.PermissionID = permissions.ID WHERE role_has_permission.RoleID = {id};";

                var DatabaseResult = SqlDataAccess.LoadData<VRoleHasPermissionModel>(sql).ToArray<VRoleHasPermissionModel>();

                foreach (var RolePermissions in DatabaseResult)
                {
                    RoleHasPermissionModel remaping = new RoleHasPermissionModel()
                    {
                        Role = new RoleModel()
                        {
                            ID = RolePermissions.RoleID,
                            Name = RolePermissions.RoleName
                        },
                        Permission = new PermissionModel()
                        {
                            ID = RolePermissions.PermissionID,
                            Name = RolePermissions.PermissionName
                        }
                    };
                    result.Add(remaping);
                }
                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "Could not find the role");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        public ActionResult CreateAndUpdateRolePermissions (ulong role_id, List<ulong>? permissions)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"DELETE FROM role_has_permission WHERE RoleID = {role_id}";
                var deleteResult = SqlDataAccess.DeleteData(sql);

                if (permissions != null)
                {
                    foreach (var permissionID in permissions)
                    {
                        VRoleHasPermissionModel data = new VRoleHasPermissionModel()
                        {
                            RoleID = role_id,
                            PermissionID = permissionID
                        };

                        sql = @"INSERT INTO role_has_permission (RoleID,PermissionID) VALUES (@RoleID,@PermissionID)";
                        var result = SqlDataAccess.SaveData<VRoleHasPermissionModel>(sql, data);
                    }
                }

                return Ok("Role updated Succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "The roles permissions failed, because of invalid data");
            }

        }
    }
}
