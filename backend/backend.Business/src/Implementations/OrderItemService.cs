using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderItemService : BaseService<OrderItem, OrderItemDto>
{
    public OrderItemService(IOrderItemRepo orderItemRepo, IMapper mapper) : base(orderItemRepo, mapper)
    {
    }
}
