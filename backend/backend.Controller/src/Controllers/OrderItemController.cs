using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controller.src.Controllers;

[Authorize]
public class OrderItemController : RootController<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>
{
    public OrderItemController(IOrderItemService baseService) : base(baseService)
    {
    }
}
