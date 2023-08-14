using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;

namespace backend.WebApi.src.RepoImplementations;

public class OrderItemRepo : BaseRepo<OrderItem>,IOrderItemRepo
{
    public OrderItemRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
