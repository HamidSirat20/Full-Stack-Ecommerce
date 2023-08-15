using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> VerifyCredentials(
        [FromBody] UserCredentialsDto credentials
    )
    {
        return Ok(await _authService.VerifyCredentials(credentials));
    }
}
