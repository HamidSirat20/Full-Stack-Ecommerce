using AutoMapper;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ReviewRepo : BaseRepo<Review>, IReviewRepo
{
    private readonly DbSet<Review> _dbSet;
    private readonly DatabaseContext _context;

    public ReviewRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Reviews;
        _context = dbContext;
    }

    public override async Task<Review?> GetOneById(Guid id)
    {
        return await _dbSet
            .Include(u => u.User)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
