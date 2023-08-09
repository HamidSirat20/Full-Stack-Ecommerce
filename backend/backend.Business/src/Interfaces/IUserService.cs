using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface IUserService : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
{
    Task<UserReadDto> UpdatePassword(string id,string password);
}
