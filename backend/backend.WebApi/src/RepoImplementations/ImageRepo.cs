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
        System.Console.WriteLine("here is product id");
        System.Console.WriteLine(image.Product);
        await _dbSet.AddAsync(image);
        return image;
    }
}
