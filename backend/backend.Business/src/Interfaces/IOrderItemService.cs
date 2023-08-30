using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface IOrderItemService
    : IBaseService<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>
{
    Task<OrderItem> CreateOrderItem(OrderItem entity);
}
