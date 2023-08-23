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
        user.Role = Role.Admin;
        await _dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdatePassword(User user)
    {
        _dbSet.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public override async Task<User> CreateOne(User entity)
    {
        entity.Role = Role.Client;

        var entry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public override async Task<User> GetOneById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }
}
