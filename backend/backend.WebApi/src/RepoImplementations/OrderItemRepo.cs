using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class OrderItemRepo : BaseRepo<OrderItem>, IOrderItemRepo
{
    private readonly DatabaseContext _dbContext;
    private readonly DbSet<OrderItem> _dbSet;

    public OrderItemRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.OrderItems;
    }

    public override async Task<OrderItem> CreateOne(OrderItem entity)
    {
        return await base.CreateOne(entity);
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderProduct()
    {
        return await _dbSet.AsNoTracking().ToArrayAsync();
    }
}
