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
            .Include(p => p.Images)
            // .Include(p => p.Reviews)
            .Include(p => p.OrderItems) // Include order items
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet

        // .ThenInclude(p => p.Product)
        // .Include(p=>p.Reviews)
        // .Include(p => p.OrderItems)
        .AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(e => e.Title.ToLower().Contains(queryParameters.Search.ToLower()));
        }

        // if (typeof(T) == typeof(Product))
        // {
        //     if (queryParameters.OrderByDescending)
        //     {
        //         items = items.OrderByDescending(
        //             e => EF.Property<DateTime>((Product)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        //     else
        //     {
        //         items = items.OrderBy(
        //             e => EF.Property<DateTime>((Product)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        // }
        // else if (typeof(T) == typeof(User))
        // {
        //     if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        //     {
        //         var orderByProperty = typeof(User).GetProperty(queryParameters.OrderBy);

        //         if (orderByProperty != null)
        //         {
        //             if (queryParameters.OrderByDescending)
        //             {
        //                 items = items.OrderByDescending(
        //                     e => EF.Property<DateTime>(e, queryParameters.OrderBy)
        //                 );
        //             }
        //             else
        //             {
        //                 items = items.OrderBy(
        //                     e => EF.Property<DateTime>(e, queryParameters.OrderBy)
        //                 );
        //             }
        //         }
        //     }
        // }
        // else if (typeof(T) == typeof(Order))
        // {
        //     if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        //     {
        //         var orderByProperty = typeof(Order).GetProperty(queryParameters.OrderBy);

        //         if (orderByProperty != null)
        //         {
        //             if (queryParameters.OrderByDescending)
        //             {
        //                 items = items.OrderByDescending(
        //                     e => EF.Property<DateTime>(e, queryParameters.OrderBy)
        //                 );
        //             }
        //             else
        //             {
        //                 items = items.OrderBy(
        //                     e => EF.Property<DateTime>(e, queryParameters.OrderBy)
        //                 );
        //             }
        //         }
        //     }
        // }

        items = items
            .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
            .Take(queryParameters.Limit);

        return items.ToList();
    }
}
