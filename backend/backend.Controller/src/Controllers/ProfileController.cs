using System.Security.Claims;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace backend.Controller.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public ProfileController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<UserReadDto>> GetProfile()
    {
        var idClaim = HttpContext.User.Claims.FirstOrDefault(
            c => c.Type == ClaimTypes.NameIdentifier
        );
        if (idClaim != null)
        {
            try
            {
                Guid idGuid = new Guid(idClaim.Value);
                var user = await _userService.GetOneById(idGuid);
                return user;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid GUID format in claim.", nameof(idClaim));
            }
        }
        return BadRequest("Invalid GUID format in claim");
    }
}
