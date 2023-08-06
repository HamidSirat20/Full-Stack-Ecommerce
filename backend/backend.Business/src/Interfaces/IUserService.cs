using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface IUserService : IBaseService<User,UserDto>
{
    UserDto UpdatePassword(string id,string password);
}
