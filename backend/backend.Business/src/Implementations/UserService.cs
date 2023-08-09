using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class UserService
    : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>,
        IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo, IMapper mapper)
        : base(userRepo, mapper)
    {
        _userRepo = userRepo;
    }

    public async Task<UserReadDto> UpdatePassword(string id, string newPassword)
    {
        var foundUser = await _userRepo.GetOneById(id);
        var user = _mapper.Map<User>(foundUser);
        var hmac = new HMACSHA256();
        var salt = hmac.Key;
        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword)).ToString();
        if (foundUser == null)
        {
            throw new Exception($"User with {id} not found");
        }
        else
        {
            user.Password = hashedPassword;
            user.Salt = salt;
            return _mapper.Map<UserReadDto>(await _userRepo.UpdatePassword(user, newPassword));
        }
    }

    public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
        entity.Password = hashedPassword;
        entity.Salt = salt;
        var created = await _userRepo.CreateOne(entity);
        return _mapper.Map<UserReadDto>(created);
    }
}
