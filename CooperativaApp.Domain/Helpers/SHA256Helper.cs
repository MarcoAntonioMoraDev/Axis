using System.Security.Cryptography;
using System.Text;

namespace CooperativaApp.Domain.Helpers
{
    public class SHA256Helper
    {
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "Value cannot be null or empty");

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
