using backend.Domain.src.Entities;

namespace backend.Domain.src.RepoInterfaces;

public interface IUserRepo : IBaseRepo<User>
{
    Task<User> CreateAdmin (User user);
    Task<User> UpdatePassword(User user, string newPassword);
    Task<User> FindUserByEmail (string email);
}
