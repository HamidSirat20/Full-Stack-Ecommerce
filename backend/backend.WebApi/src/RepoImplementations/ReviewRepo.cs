using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ReviewRepo : BaseRepo<Review>, IReviewRepo
{
    private readonly DbSet<Review> _dbSet;

    public ReviewRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Reviews;
    }

    public override async Task<IEnumerable<Review>> GetAll(QueryParameters queryParameters)
    {
        //return await _dbSet.AsNoTracking().ToArrayAsync();
        var items = _dbSet
            .Include(r => r.Product)
            .Include(r => r.User)
            //.Include(i=>i.Images)
            .AsEnumerable();
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items
                .Where(e =>
                {
                    if (e is Review review && !string.IsNullOrEmpty(review.Comment))
                    {
                        return review.Comment.ToLower().Contains(queryParameters.Search.ToLower());
                    }
                    return false;
                })
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            var orderByProperty = typeof(Review).GetProperty(queryParameters.OrderBy);

            if (orderByProperty != null)
            {
                if (queryParameters.OrderByDescending)
                {
                    items = items.OrderByDescending(e => orderByProperty.GetValue(e));
                }
                else
                {
                    items = items.OrderBy(e => orderByProperty.GetValue(e));
                }
            }
        }

        items = items
            .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
            .Take(queryParameters.Limit);

        return items.ToList();
    }

     public override async Task<Review?> GetOneById(Guid id)
    {
        return await _dbSet
            .Include(u => u.User)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
