namespace backend.Business.src.Interfaces;

public interface IPasswordService
{
    void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt);
    bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt);
}
