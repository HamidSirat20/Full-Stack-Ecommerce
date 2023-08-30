using AutoMapper;
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
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepo userRepo, IMapper mapper,IPasswordService passwordService)
        : base(userRepo, mapper)
    {
        _userRepo = userRepo;
        _passwordService = passwordService;
    }

    public async Task<UserReadDto> UpdatePassword(Guid id, string newPassword)
    {
        var foundUser = await _userRepo.GetOneById(id);
        if (foundUser == null)
        {
            throw new Exception("User not found");
        }
        _passwordService.HashPassword(newPassword, out var hashedPassword, out var salt);
        foundUser.Password = hashedPassword;
        foundUser.Salt = salt;
        return _mapper.Map<UserReadDto>(await _userRepo.UpdatePassword(foundUser));
    }

    public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        _passwordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
        entity.Password = hashedPassword;
        entity.Salt = salt;
        var created = await _userRepo.CreateOne(entity);
        return _mapper.Map<UserReadDto>(created);
    }

    public async Task<UserReadDto> CreateAdmin(UserCreateDto userCreate)
    {
         var entity = _mapper.Map<User>(userCreate);
        _passwordService.HashPassword(userCreate.Password, out var hashedPassword, out var salt);
        entity.Password = hashedPassword;
        entity.Salt = salt;
        var created = await _userRepo.CreateAdmin(entity);
        return _mapper.Map<UserReadDto>(created);
    }


}
