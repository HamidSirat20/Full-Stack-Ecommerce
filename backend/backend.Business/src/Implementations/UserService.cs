using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class UserService : BaseService<User, UserDto>,IUserService
{
    private readonly IUserRepo _userRepo;
    public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
    {
        _userRepo = userRepo;
    }

    public UserDto UpdatePassword(string id, string password)
    {
        var foundUser = _userRepo.GetOneById(id);
        if (foundUser == null)
        {
            throw new Exception($"User with {id} not found");
        }
        else
        {
            return _mapper.Map<UserDto>(_userRepo.UpdatePassword(foundUser,password));
        }
    }
}
