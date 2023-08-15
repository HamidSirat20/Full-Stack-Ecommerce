using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

// [Authorize]
public class UserController : RootController<User, UserReadDto, UserCreateDto, UserUpdateDto>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService baseService, IMapper mapper)
        : base(baseService)
    {
        _userService = baseService;
        _mapper = mapper;
    }

    [HttpPost("password")]
    public async Task<ActionResult<UserReadDto>> UpdatePassword(
        Guid id,
        [FromBody] PasswordUpdateDto passwordUpdateDto
    )
    {
        try
        {
            var updatedUser = await _userService.UpdatePassword(id, passwordUpdateDto.Password);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            var userReadDto = _mapper.Map<UserReadDto>(updatedUser);
            return Ok(userReadDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Admin")]
    public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto userCreateDto)
    {
        var createdObject = await _userService.CreateAdmin(userCreateDto);
        return CreatedAtAction(nameof(CreateAdmin), createdObject);
    }

    [Authorize]
    public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        return Ok(await _userService.GetAll(queryParameters));
    }
}
