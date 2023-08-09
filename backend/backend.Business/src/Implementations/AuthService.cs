using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class AuthService : IAuthService
{
    private IUserRepo _userRepo;
    public AuthService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    public async Task<string> VerifyCredentials(UserCredentialsDto userCredentialsDto)
    {
        var foundUser = await _userRepo.FindUserByEmail(userCredentialsDto.Password);
       var isAuthenticated =  PasswordService.VerifyPassword(userCredentialsDto.Password,foundUser.Password,foundUser.Salt);
    }
}
