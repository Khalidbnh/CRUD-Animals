using System.Security.Cryptography;
using System.Text;

namespace WebApplication9.Tools

{
    public static class PasswordHelper
    {

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                Console.WriteLine("Generated PasswordHash: " + BitConverter.ToString(passwordHash));
                Console.WriteLine("Generated PasswordSalt: " + BitConverter.ToString(passwordSalt));
            }
        }



        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash); // Compare hashes
            }
        }
    }
}
