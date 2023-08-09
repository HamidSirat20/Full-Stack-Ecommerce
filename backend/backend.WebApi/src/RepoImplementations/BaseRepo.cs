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

    public async Task<T> CreateOne(T newEntity)
    {
        var entityEntry = await _dbSet.AddAsync(newEntity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<bool> DeleteOneById(T newEntity)
    {
        _dbSet.Remove(newEntity);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<T>> GetAll(QueryParameters queryParameters)
    {
        var entities = _dbSet.AsQueryable();
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            entities = entities.Where(
                entity =>
                    entity
                        .ToString()
                        .Contains(queryParameters.Search, StringComparison.OrdinalIgnoreCase)
            );
        }
        var propertyInfo = typeof(T).GetProperty(
            queryParameters.OrderBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
        );
        if (propertyInfo != null)
        {
            entities = queryParameters.OrderByDescending
                ? entities.OrderByDescending(entity => propertyInfo.GetValue(entity))
                : entities.OrderBy(entity => propertyInfo.GetValue(entity));
        }
        entities = entities.Skip(queryParameters.Offset - 1).Take(queryParameters.Limit);

        return await entities.ToListAsync();
    }

    public async Task<T> GetOneById(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> UpdateOneById(T foundEntity, T newEntity)
    {
        _context.Entry(foundEntity).CurrentValues.SetValues(newEntity);
        await _context.SaveChangesAsync();
        return foundEntity;
    }
}
