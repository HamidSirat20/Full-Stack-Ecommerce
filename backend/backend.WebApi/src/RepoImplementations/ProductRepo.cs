using backend.Domain.src.Common;
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
        return _dbSet
            .Include(p => p.OrderItems)
            .Include(p => p.Reviews)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .FirstOrDefault(i => i.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet
            .Include(p => p.OrderItems)
            .Include(p => p.Reviews)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(e => e.Title.ToLower().Contains(queryParameters.Search.ToLower()));
        }
        items = items
            .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
            .Take(queryParameters.Limit);

        return items.ToList();
    }
}
