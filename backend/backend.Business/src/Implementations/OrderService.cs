using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderService : BaseService<Order, OrderReadDto,OrderCreateDto,OrderUpdateDto>,IOrderService
{
    public OrderService(IOrderRepo orderRepo, IMapper mapper) : base(orderRepo, mapper)
    {
    }
}
