using System.Text;
using System.Security.Cryptography;

namespace Vextech_APP
{
    public class Crypto
    {
        public static string HashwithSalt(string email, string password)
        {
            string emailHash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(email.Split("@")[0])));
            string passwordHash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));

            return emailHash+passwordHash;
        }
    }
}
