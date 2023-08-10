using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    private readonly DbSet<User> _dbSet;
    private readonly DatabaseContext _context;

    public UserRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Users;
        _context = dbContext;
    }

    public async Task<User> CreateAdmin(User user)
    {
        var adminCreated = await _dbSet.AddAsync(user);
        _context.SaveChanges();
        return adminCreated.Entity;
    }

    public async Task<User> FindUserByEmail(string email)
    {
        var userFound = await _dbSet.FindAsync(email);
        return userFound;
    }

    public async Task<User> UpdatePassword(User user, string newPassword)
    {
        var foundUser = _dbSet.FirstOrDefault(u => u.Id == user.Id);
        if (foundUser != null)
        {
            foundUser.Password = newPassword;
            await _context.SaveChangesAsync();
        }

        return foundUser;
    }
}
