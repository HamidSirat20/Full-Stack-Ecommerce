using System.Security.Cryptography;
using System.Text;

using backend.Business.src.Interfaces;

namespace backend.Business.src.Common;

public class PasswordService : IPasswordService
    {
        public void HashPassword(
            string originalPassword,
            out string hashedPassword,
            out byte[] salt
        )
        {
            using (var hmac = new HMACSHA256())
            {
                salt = hmac.Key;
                hashedPassword = Encoding.UTF8.GetString(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))
                );
            }
        }

        public bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                var hashedOriginal = Encoding.UTF8.GetString(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))
                );
                return hashedOriginal == hashedPassword;
            }
        }
    }