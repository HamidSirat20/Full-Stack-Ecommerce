using System.Security.Cryptography;
using System.Text;

namespace backend.Business.src.Common;

public class PasswordService
{
    public static void HashPassword(string oldPassword, out string hashedPassword, out byte[] salt)
    {
        var hmac = new HMACSHA256();
        salt = hmac.Key;
        hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(oldPassword)).ToString();
    }

    public static bool VerifyPassword(string oldPassword, string hashedPassword, byte[] salt)
    {
        var hmac = new HMACSHA256(salt);
        var hashNewPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(oldPassword)).ToString();
        return hashNewPassword == hashedPassword;
    }
}
