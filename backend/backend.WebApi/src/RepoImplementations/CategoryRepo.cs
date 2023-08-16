using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;

namespace backend.WebApi.src.RepoImplementations;

public class CategoryRepo : BaseRepo<Category>,ICategoryRepo
{
    public CategoryRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
