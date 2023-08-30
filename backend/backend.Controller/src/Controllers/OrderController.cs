using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

[Authorize]
public class OrderController : RootController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService baseService, IAuthorizationService authService)
        : base(baseService)
    {
        _authorizationService = authService;
        _orderService  = baseService;
    }

    // [Authorize]
    // public override async Task<ActionResult<OrderReadDto>> UpdateOne(
    //     [FromRoute] Guid id,
    //     [FromBody] OrderUpdateDto updateDto
    // )
    // {
    //     var user = HttpContext.User;
    //     var order = await _orderService.GetOneById(id);
    //     var authorizedOwner = await _authorizationService.AuthorizeAsync(user, order, "OwnerOnly");
    //     if (authorizedOwner.Succeeded)
    //     {
    //         return await _orderService.UpdateOneById(id, updateDto);
    //     }
    //     else
    //     {
    //         return new ForbidResult();
    //     }
    // }

    // [Authorize]
    // public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
    // {
    //     var user = HttpContext.User;
    //     var order = await _orderService.GetOneById(id);
    //     var authorizedOwner =await _authorizationService.AuthorizeAsync(user, order, "OwnerOnly");
    //     if (authorizedOwner.Succeeded)
    //     {
    //         return await _orderService.DeleteOneById(id);
    //     }
    //     else
    //     {
    //         return new ForbidResult();
    //     }
    // }
}
