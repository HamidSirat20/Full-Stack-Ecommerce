using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;

namespace backend.WebApi.src.RepoImplementations;

public class ImageRepo : BaseRepo<Image>,IImageRepo
{
    public ImageRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}