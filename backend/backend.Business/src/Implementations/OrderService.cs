using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderService
    : BaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>,
        IOrderService
{
    private readonly IOrderRepo _orderRepo;
    private readonly IUserRepo _userRepo;
    private readonly IOrderItemService _orderItemService;
    private readonly IProductRepo _productRepo;
    private readonly IOrderItemRepo _orderItemRepo;

    public OrderService(
        IOrderRepo orderRepo,
        IMapper mapper,
        IUserRepo userRepo,
        IOrderItemRepo orderItemRepo,
        IOrderItemService orderItemService,
        IProductRepo productRepo
    )
        : base(orderRepo, mapper)
    {
        _orderRepo = orderRepo;
        _userRepo = userRepo;
        _orderItemRepo = orderItemRepo;
        _productRepo = productRepo;
        _orderItemService = orderItemService;
    }

    public override async Task<OrderReadDto> CreateOne(OrderCreateDto entity)
    {
        var user = await _userRepo.GetOneById(entity.UserId);
            if (user == null)
            {
                throw CustomErrorHandler.NotFoundException();
            }

            var order = new Order
            {
                User = user,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            var createdOrder = await _orderRepo.CreateOne(order);

            var orderProducts = _mapper.Map<List<OrderItem>>(entity.OrderProducts);

            for(int i = 0; i < orderProducts.Count(); i++ ) {
                var orderProductAtCurrentIndex = orderProducts.ElementAt(i);
                orderProductAtCurrentIndex.Order = createdOrder;
                orderProductAtCurrentIndex.Product = await _productRepo.GetOneById(entity.OrderProducts.ElementAt(i).ProductId);

                await _orderItemService.CreateOrderItem(orderProductAtCurrentIndex);
            }

            var orderReadDto = new OrderReadDto {
                UserId = order.User.Id,
                Status = order.Status,
                orderItems = _mapper.Map<List<OrderItemCreateDto>>(order.OrderItems)
            };

            return orderReadDto;
        }
    }

