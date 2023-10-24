using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderItemService
    : BaseService<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>,
        IOrderItemService
{
    private readonly IOrderItemRepo _orderItemRepo;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepo orderItemRepo, IMapper mapper)
        : base(orderItemRepo, mapper)
    {
        _orderItemRepo = orderItemRepo;
        _mapper = mapper;
    }

    // public async Task<OrderItemReadDto> CreateOrderItem(OrderItemCreateDto orderItemDto)
    // {
    //     var orderItem = _mapper.Map<OrderItem>(orderItemDto);
    //     return _mapper.Map<OrderItemReadDto>(await _orderItemRepo.CreateOne(orderItem));
    // }
}
