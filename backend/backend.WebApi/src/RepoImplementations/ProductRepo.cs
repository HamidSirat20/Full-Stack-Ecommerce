using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;

namespace backend.WebApi.src.RepoImplementations;

public class ProductRepo : BaseRepo<Product>,IProductRepo
{
    public ProductRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
