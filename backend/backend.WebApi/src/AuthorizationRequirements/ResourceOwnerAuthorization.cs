using System.Security.Claims;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace backend.WebApi.src.AuthorizationRequirements;

public class ResourceOwnerAuthorization : IAuthorizationRequirement
{

}

public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerAuthorization, Order>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerAuthorization requirement, Order resource)
    {
        var owner = context.User;
        var userId = owner.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if(resource.User.Id.ToString() == userId && resource.Status.ToString() =="Pending")
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
