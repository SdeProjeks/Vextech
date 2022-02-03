using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using MySqlConnector;

namespace Vextech_API.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "server=localhost;user id=root;Pwd=password;database=mvccallwebapih3;persistsecurityinfo=True; allowuservariables=True")
        {
            return connectionName;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static object SaveDataThatReturnsId<T>(string sql, T data)
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                return cnn.ExecuteScalar(sql, data);
            }
        }

        public static int DeleteData(string sql)
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql);
            }
        }

        public static int UpdateData(string sql)
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql);
            }
        }
    }
}