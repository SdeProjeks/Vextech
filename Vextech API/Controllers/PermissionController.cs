using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;

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
                string sql;
                sql = $"SELECT * FROM permissions";

                var result = SqlDataAccess.LoadData<PermissionModel>(sql);

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<PermissionModel>> GetPermissionByID(int id)
        {
            try
            {
                string sql;
                sql = $"SELECT * FROM permissions WHERE ID = {id}";

                var result = SqlDataAccess.LoadData<PermissionModel>(sql);

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public ActionResult<int> CreatePermission(string name)
        {
            try
            {
                PermissionModel data = new PermissionModel()
                {
                    Name = name
                };

                string sql;
                sql = @"INSERT INTO permissions (Name) VALUES (@Name);";

                var result = SqlDataAccess.SaveData<PermissionModel>(sql, data);

                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut]
        public ActionResult<int> UpdatePermissionByID(int id, string name)
        {
            try
            {
                PermissionModel data = new PermissionModel()
                {
                    ID = id,
                    Name = name
                };

                string sql;
                sql = @"INSERT INTO permissions (Name) VALUES (@Name);";

                var result = SqlDataAccess.UpdateData(sql);

                return result;
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
