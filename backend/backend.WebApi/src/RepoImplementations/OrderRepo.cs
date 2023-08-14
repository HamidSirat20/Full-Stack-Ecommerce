using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;

namespace backend.WebApi.src.RepoImplementations;

public class OrderRepo : BaseRepo<Order>,IOrderRepo
{
    public OrderRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
