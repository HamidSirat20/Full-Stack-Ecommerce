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

    public virtual async Task<bool> DeleteOneById(T newEntity)
    {
        _dbSet.Remove(newEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<T> GetOneById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> UpdateOneById(T newEntity)
    {
        _dbSet.Update(newEntity);
        await _context.SaveChangesAsync();
        return newEntity;
    }

    public virtual async Task<IEnumerable<T>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(e =>
            {
                if (
                    e is User user
                    && !string.IsNullOrEmpty(user.FirstName)
                    && !string.IsNullOrEmpty(user.LastName)
                    && (
                        user.FirstName.ToLower().Contains(queryParameters.Search.ToLower())
                        || user.LastName.ToLower().Contains(queryParameters.Search.ToLower())
                    )
                )
                {
                    return true;
                }
                else if (e is Product product && !string.IsNullOrEmpty(product.Title))
                {
                    return product.Title.ToLower().Contains(queryParameters.Search.ToLower());
                }
                else if (e is Order order && !string.IsNullOrEmpty(order.Status.ToString()))
                {
                    return order.Status.ToString().Contains(queryParameters.Search.ToLower());
                }
                else if (e is Review review && !string.IsNullOrEmpty(review.Comment.ToString()))
                {
                    return review.Comment.ToString().Contains(queryParameters.Search.ToLower());
                }
                else if (
                    e is Category categ && !string.IsNullOrEmpty(categ.CategoryName.ToString())
                )
                {
                    return categ.CategoryName.ToString().Contains(queryParameters.Search.ToLower());
                }
                return false;
            });
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
