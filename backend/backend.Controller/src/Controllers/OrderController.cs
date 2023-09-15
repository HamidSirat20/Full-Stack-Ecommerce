using System.Security.Claims;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
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
        _orderService = baseService;
    }

    [Authorize]
    public override async Task<ActionResult<OrderReadDto>> UpdateOne(
        [FromRoute] Guid id,
        [FromBody] OrderUpdateDto update
    )
    {
        var user = HttpContext.User;
        var order = await _orderService.GetOneById(id);
        var authorizeOwner = await _authorizationService.AuthorizeAsync(user, order, "OwnerOnly");
        Console.WriteLine("authorizeOwner " + authorizeOwner.Succeeded);
        if (authorizeOwner.Succeeded)
        {
            return await base.UpdateOne(id, update);
        }
        else
        {
            return new ForbidResult();
        }
    }

    [Authorize]
    public override async Task<ActionResult<OrderReadDto>> CreateOne(
        [FromBody] OrderCreateDto orderDto
    )
    {
        var authenticatedUser = HttpContext.User;
        var userId = authenticatedUser
            .FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!
            .Value;

        return await _orderService.CreateOne(new Guid(userId), orderDto);
    }

    [Authorize]
    [HttpGet]
    public override async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        var result = (await _orderService.GetAll(queryParameters)).ToArray();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<OrderReadDto>> GetOneById([FromRoute] Guid id)
    {
        return Ok(await _orderService.GetOneById(id));
    }

    [Authorize]
    [HttpDelete("{id:Guid}")]
    public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
    {
        return StatusCode(204, await _orderService.DeleteOneById(id));
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
