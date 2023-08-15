using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using Microsoft.IdentityModel.Tokens;

namespace backend.Business.src.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepo userRepo,IPasswordService passwordService)
    {
        _userRepo = userRepo;
        _passwordService = passwordService;
    }

    public async Task<string> VerifyCredentials(UserCredentialsDto credentials)
    {
        var foundUserByEmail =
            await _userRepo.FindUserByEmail(credentials.Email)
            ?? throw new Exception("Email not found");
        var isAuthenticated = _passwordService.VerifyPassword(
            credentials.Password,
            foundUserByEmail.Password,
            foundUserByEmail.Salt
        );
        if (!isAuthenticated)
        {
            throw new Exception("user not found");
        }
        return GenerateToken(foundUserByEmail);
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("my-secret-key-is-unique-and-should-keep-it-safe")
        );
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256Signature
        );
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "backend",
            Expires = DateTime.UtcNow.AddMinutes(30),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
