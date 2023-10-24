using System.Reflection;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    private readonly DbSet<Product> _dbSet;
    private readonly DatabaseContext _dbContext;

    public ProductRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Products;
        _dbContext = dbContext;
    }

    public override async Task<Product?> GetOneById(Guid id)
    {
        return _dbSet
            .Include(p => p.Reviews)
            .Include(p => p.Category)
            .FirstOrDefault(i => i.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet.Include(p => p.Reviews).Include(p => p.Category).AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(e => e.Title.ToLower().Contains(queryParameters.Search.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            var propertyInfo = typeof(Product).GetProperty(
                queryParameters.OrderBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );
            if (propertyInfo != null)
            {
                items = queryParameters.OrderByDescending
                    ? items.OrderByDescending(p => propertyInfo.GetValue(p, null))
                    : items.OrderBy(p => propertyInfo.GetValue(p, null));
            }
        }
        items = items.Skip(queryParameters.Offset - 1).Take(queryParameters.Limit);

        return items.ToList();
    }

    public override async Task<Product> CreateOne(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.ModifiedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
}
