using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderItemService : BaseService<OrderItem, OrderItemReadDto,OrderItemCreateDto,OrderItemUpdateDto>,IOrderItemService
{
    public OrderItemService(IOrderItemRepo orderItemRepo, IMapper mapper) : base(orderItemRepo, mapper)
    {
    }
}
