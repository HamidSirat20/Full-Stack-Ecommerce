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
        return await _dbSet
            .Include(rev => rev.Reviews)
            .Include(prodId => prodId.Id)
            .Include(orderItem => orderItem.OrderItems)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet
            .Include(r => r.Reviews)
            .ThenInclude(r => r.User)
            //.Include(i=>i.Images)
            .AsEnumerable();
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items
                .Where(e =>
                {
                    if (e is Product product && !string.IsNullOrEmpty(product.Title))
                    {
                        return product.Title.ToLower().Contains(queryParameters.Search.ToLower());
                    }
                    return false;
                })
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            var orderByProperty = typeof(Product).GetProperty(queryParameters.OrderBy);

            if (orderByProperty != null)
            {
                items = queryParameters.OrderByDescending
                    ? items.OrderByDescending(e => orderByProperty.GetValue(e))
                    : items.OrderBy(e => orderByProperty.GetValue(e));
            }
        }

        items = items
            .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
            .Take(queryParameters.Limit);

        return items.ToList();
    }
}
