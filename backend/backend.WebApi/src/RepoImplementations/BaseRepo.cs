using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class BaseRepo<T> : IBaseRepo<T>
    where T : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepo(DatabaseContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _context = dbContext;
    }

    public virtual async Task<T> CreateOne(T newEntity)
    {
        await _dbSet.AddAsync(newEntity);
        await _context.SaveChangesAsync();
        return newEntity;
    }

    public async Task<bool> DeleteOneById(T newEntity)
    {
        _dbSet.Remove(newEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<T> GetOneById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> UpdateOneById(T newEntity)
    {
        _dbSet.Update(newEntity);
        await _context.SaveChangesAsync();
        return newEntity;
    }

    public async Task<IEnumerable<T>> GetAll(QueryParameters queryParameters)
    {
        //return await _dbSet.AsNoTracking().ToArrayAsync();
        var items = _dbSet.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(e =>
            {
                if (e is User user && !string.IsNullOrEmpty(user.FirstName))
                {
                    return user.FirstName.ToLower().Contains(queryParameters.Search.ToLower());
                }
                else if (e is Product product && !string.IsNullOrEmpty(product.Title))
                {
                    return product.Title.ToLower().Contains(queryParameters.Search.ToLower());
                }
                 else if (e is Order order && !string.IsNullOrEmpty(order.Status.ToString()))
                {
                    return order.Status.ToString().Contains(queryParameters.Search.ToLower());
                }
                return false;
            });
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            var orderByProperty = typeof(T).GetProperty(queryParameters.OrderBy);

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
