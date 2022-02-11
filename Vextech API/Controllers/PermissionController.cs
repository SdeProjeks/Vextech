using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using System.Reflection;

namespace Vextech_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class Permission : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<PermissionModel>> GetPermissions()
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT * FROM permissions";

                var result = SqlDataAccess.LoadData<PermissionModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "There are no permissions");
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
        public ActionResult<List<PermissionModel>> GetPermissionByID(int id)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                string sql;
                sql = $"SELECT * FROM permissions WHERE ID = {id}";

                var result = SqlDataAccess.LoadData<PermissionModel>(sql);

                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "Could not find the permission");
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
        public ActionResult CreatePermission(string name)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                PermissionModel data = new PermissionModel()
                {
                    Name = name
                };

                string sql;
                sql = @"INSERT INTO permissions (Name) VALUES (@Name);";

                var result = SqlDataAccess.SaveData<PermissionModel>(sql, data);
                
                return Ok("Created the permission succcesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status400BadRequest, "Created failed because of invalid input");
            }
        }

        [HttpPut]
        public ActionResult UpdatePermissionByName(ulong id, string name)
        {
            try
            {
                LogsController.CreateCalledLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com");

                PermissionModel data = new PermissionModel()
                {
                    ID = id,
                    Name = name
                };

                string sql;
                sql = $"UPDATE permissions SET Name = '{name}' WHERE ID = {id}";

                var result = SqlDataAccess.UpdateData(sql);

                return Ok("permission Updated succesfully");
            }
            catch (Exception ex)
            {
                LogsController.CreateExceptionLog(MethodBase.GetCurrentMethod().Name, "Placeholser@gmail.com", ex);

                return this.StatusCode(StatusCodes.Status404NotFound, "Update failed either because of invalid data or we could not find it");
            }
        }
    }
}
