using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controller.src.Controllers;

[Authorize]
public class OrderController : RootController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
{
    public OrderController(IOrderService baseService) : base(baseService)
    {
    }
}
