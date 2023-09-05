using backend.Business.src.Dtos;
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

    public async Task<OrderItem> CreateOrderProduct(OrderItem orderItem)
    {
        orderItem.CreatedAt = DateTime.UtcNow;
        orderItem.ModifiedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(orderItem);
        await _dbContext.SaveChangesAsync();

        return orderItem;
    }

    public async Task<bool> DeleteOneById(OrderItem item)
    {
        var foundOrderProduct = _dbSet.Remove(item);
        if (foundOrderProduct != null)
        {
            _dbContext.SaveChanges();
            return true;
        }
        throw new Exception("OrderProduct not found");
    }
    public OrderItem UpdateOneById(OrderItem originalEntity, OrderItemUpdateDto updatedEntity)
    {
        originalEntity.Amount = updatedEntity.Amount;
        originalEntity.ProductId = updatedEntity.ProductId;
        originalEntity.ModifiedAt = DateTime.UtcNow;
        _dbContext.SaveChanges();
        return originalEntity;
    }
}
