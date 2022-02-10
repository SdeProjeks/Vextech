using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using MySqlConnector;

namespace Vextech_API.DataAccess
{
    public static class SqlDataAccess
    {
        
        public static string GetConnectionString()
        {
            //find and converts appsettings.json to valus/key
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            // get the connectionstring
            return config.GetValue<string>("ConnectionStrings:MySQLConnection");

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