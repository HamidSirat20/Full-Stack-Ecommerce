using AutoMapper;
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
    private readonly IOrderItemRepo _orderItemRepo;

    public OrderService(IOrderRepo orderRepo, IOrderItemRepo orderItemRepo, IMapper mapper)
        : base(orderRepo, mapper)
    {
        _orderRepo = orderRepo;
        _orderItemRepo = orderItemRepo;
    }

    public async Task<OrderReadDto> CreateOne(Guid userId, OrderCreateDto orderDto)
    {
        try
        {
            var order = new Order
            {
                UserId = userId,
                Status = orderDto.Status,
                ShippingAddress = orderDto.ShippingAddress
            };
            var createdOrder = await _orderRepo.CreateOne(order);

            createdOrder.OrderItems = new List<OrderItem>();
            foreach (var item in orderDto.OrderProducts)
            {
                var orderProduct = new OrderItem
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    OrderId = createdOrder.Id,
                    UserId = createdOrder.UserId
                };
                createdOrder.OrderItems.Add(orderProduct);

                var createdOrderProduct = await _orderItemRepo.CreateOne(orderProduct);
            }

            var returnOrder = _mapper.Map<OrderReadDto>(createdOrder);

            return returnOrder;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"An error occurred while creating an order: {ex.Message}");
            if (ex.InnerException != null)
            {
                System.Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }
}
