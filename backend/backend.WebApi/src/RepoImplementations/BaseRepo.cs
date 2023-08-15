using System.Reflection;
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

    public async Task<IEnumerable<T>> GetAll(QueryParameters queryParameters)
    {
        return await _dbSet.ToArrayAsync();
        // var query = _dbSet.AsQueryable();

        // if (!string.IsNullOrEmpty(queryParameters.Search))
        // {
        //     if (typeof(T) == typeof(Product))
        //     {
        //         query = query.Where(
        //             e => ((Product)(object)e).Title.Contains(queryParameters.Search)
        //         );
        //     }
        //     else if (typeof(T) == typeof(User))
        //     {
        //         query = query.Where(
        //             e =>
        //                 ((User)(object)e).FirstName.Contains(queryParameters.Search)
        //                 || ((User)(object)e).LastName.Contains(queryParameters.Search)
        //         );
        //     }
        //     else if (typeof(T) == typeof(Order))
        //     {
        //         query = query.Where(
        //             e => ((Order)(object)e).OrderItems!.ToString().Contains(queryParameters.Search)
        //         );
        //     }
        // }

        // if (queryParameters.OrderByDescending)
        // {
        //     if (typeof(T) == typeof(Product))
        //     {
        //         query = query.OrderByDescending(
        //             e => EF.Property<DateTime>((Product)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        //     else if (typeof(T) == typeof(User))
        //     {
        //         query = query.OrderByDescending(
        //             e => EF.Property<DateTime>((User)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        //     else if (typeof(T) == typeof(Order))
        //     {
        //         query = query.OrderByDescending(
        //             e => EF.Property<DateTime>((Order)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        // }
        // else
        // {
        //     if (typeof(T) == typeof(Product))
        //     {
        //         query = query.OrderBy(
        //             e => EF.Property<DateTime>((Product)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        //     else if (typeof(T) == typeof(User))
        //     {
        //         query = query.OrderBy(
        //             e => EF.Property<DateTime>((User)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        //     else if (typeof(T) == typeof(Order))
        //     {
        //         query = query.OrderBy(
        //             e => EF.Property<DateTime>((Order)(object)e, queryParameters.OrderBy)
        //         );
        //     }
        // }

        // query = query
        //     .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
        //     .Take(queryParameters.Limit);

        // return await query.ToListAsync();
    }

    public async Task<T> GetOneById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> UpdateOneById(T newEntity)
    {
        _dbSet.Update(newEntity);
        await _context.SaveChangesAsync();
        return newEntity;
    }
}
