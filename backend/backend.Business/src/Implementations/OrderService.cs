using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderService : BaseService<Order, OrderDto>
{
    public OrderService(IOrderRepo orderRepo, IMapper mapper) : base(orderRepo, mapper)
    {
    }
}
