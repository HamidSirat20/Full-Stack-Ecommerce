using backend.Business.src.Dtos;

namespace backend.Business.src.Interfaces;

public interface IAuthService
{
    Task<string> VerifyCredentials(UserCredentialsDto userCredentialsDto);
}
