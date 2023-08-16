using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    private readonly DbSet<Product> _dbSet;

    public ProductRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Products;
    }

    public override async Task<Product?> GetOneById(Guid id)
    {
        return await _dbSet
            .Include(rev => rev.Reviews)
            .Include(orderItem=>orderItem.OrderItems)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
