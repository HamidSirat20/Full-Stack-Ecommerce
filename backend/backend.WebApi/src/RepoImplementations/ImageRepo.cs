using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ImageRepo : BaseRepo<Image>, IImageRepo
{
    private readonly DbSet<Image> _dbSet;
    private readonly DatabaseContext _context;

    public ImageRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Images;
        _context = dbContext;
    }

    public override async Task<Image> CreateOne(Image image)
    {
        await _dbSet.AddAsync(image);
        await _context.SaveChangesAsync();
        return image;
    }

    public override async Task<Image> UpdateOneById(Image image)
    {
        _dbSet.Update(image);
        await _context.SaveChangesAsync();
        return image;
    }

    public override async Task<Image> GetOneById(Guid id)
    {
        return await _dbSet.Include(product => product.Product).FirstAsync(i => i.Id == id);
    }

    public override async Task<IEnumerable<Image>> GetAll(QueryParameters queryParameters)
    {
        //return await _dbSet.AsNoTracking().ToArrayAsync();
        var items = _dbSet.Include(r => r.Product).AsEnumerable();
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            if (Guid.TryParse(queryParameters.Search, out Guid searchGuid))
            {
                items = items.Where(e =>
                {
                    if (e is Image image)
                    {
                        return image.Id == searchGuid;
                    }
                    return false;
                });
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            var orderByProperty = typeof(Image).GetProperty(queryParameters.OrderBy);

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
