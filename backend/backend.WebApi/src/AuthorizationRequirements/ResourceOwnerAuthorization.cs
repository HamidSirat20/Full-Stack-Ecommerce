using System.Security.Claims;
using backend.Business.src.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace backend.WebApi.src.AuthorizationRequirements;

public class ResourceOwnerAuthorization : IAuthorizationRequirement { }

public class ResourceOwnerAuthorizationHandler
    : AuthorizationHandler<ResourceOwnerAuthorization, OrderReadDto>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerAuthorization requirement,
        OrderReadDto resource
    )
    {
        var owner = context.User;
        var userId = owner.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (resource.User.Id.ToString() == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
