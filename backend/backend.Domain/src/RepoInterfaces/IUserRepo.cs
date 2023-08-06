using backend.Domain.src.Entities;

namespace backend.Domain.src.RepoInterfaces;

public interface IUserRepo : IBaseRepo<User>
{
    User CreateAdmin (User user);
    User UpdatePassword(User user, string newPassword);
}
