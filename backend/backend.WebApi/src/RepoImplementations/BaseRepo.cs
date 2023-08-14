using System.Reflection;
using backend.Domain.src.Common;
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
        // var entities = _dbSet.AsQueryable();
        // if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        // {
        //     entities = entities.Where(
        //         entity =>
        //             entity
        //                 .ToString()
        //                 .Contains(queryParameters.Search, StringComparison.OrdinalIgnoreCase)
        //     );
        // }
        // var propertyInfo = typeof(T).GetProperty(
        //     queryParameters.OrderBy,
        //     BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
        // );
        // if (propertyInfo != null)
        // {
        //     entities = queryParameters.OrderByDescending
        //         ? entities.OrderByDescending(entity => propertyInfo.GetValue(entity))
        //         : entities.OrderBy(entity => propertyInfo.GetValue(entity));
        // }
        // entities = entities.Skip(queryParameters.Offset - 1).Take(queryParameters.Limit);

        // return await entities.ToListAsync();
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
