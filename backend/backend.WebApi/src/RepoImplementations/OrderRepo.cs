using backend.Business.src.Dtos;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class OrderRepo : BaseRepo<Order>, IOrderRepo
{
    private readonly DbSet<Order> _dbSet;
    private readonly DatabaseContext _dbContext;

    public OrderRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _dbSet = dbContext.Orders;
        _dbContext = dbContext;
    }

    public override async Task<Order?> GetOneById(Guid id)
    {
        return _dbSet
            .Include(o => o.OrderItems)
            .Include(i => i.User)
            .FirstOrDefault(i => i.Id == id);
    }

    public override async Task<IEnumerable<Order>> GetAll(QueryParameters queryParameters)
    {
        var items = _dbSet.Include(p => p.OrderItems).Include(i => i.User).AsEnumerable();

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            items = items.Where(
                e => e.ShippingAddress.ToLower().Contains(queryParameters.Search.ToLower())
            );
        }
        items = items
            .Skip((queryParameters.Offset - 1) * queryParameters.Limit)
            .Take(queryParameters.Limit);

        return items.ToList();
    }

    public override async Task<Order> CreateOne(Order order)
    {
        order.CreatedAt = DateTime.UtcNow;
        order.ModifiedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateOneById(Order originalEntity, OrderUpdateDto updatedEntity)
    {
        originalEntity.Status = updatedEntity.Status;
        originalEntity.ModifiedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return originalEntity;
    }
}
