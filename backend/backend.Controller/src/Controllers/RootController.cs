using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controller.src.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class RootController<T, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
{
    private readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _baseService;

    public RootController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> baseService)
    {
        _baseService = baseService;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        var result = (await _baseService.GetAll(queryParameters)).ToArray();
        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDto>> GetOneById([FromRoute] Guid id)
    {
        var foundEntity = await _baseService.GetOneById(id);

        if (foundEntity == null)
        {
            return NotFound();
        }

        return Ok(foundEntity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TReadDto>> CreateOne([FromBody] TCreateDto createDto)
    {
        try
        {
             var createdObject = await _baseService.CreateOne(createDto);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException;
            return StatusCode(500, " Hahaha An error occurred while saving the entity changes."+innerException);
        }
    }

    [HttpPatch("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDto>> UpdateOne(
        [FromRoute] Guid id,
        [FromBody] TUpdateDto updateDto
    )
    {
        return await _baseService.UpdateOneById(id, updateDto);
    }

    [HttpDelete("{id:Guid}")]
    public virtual async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
    {
        return StatusCode(204, await _baseService.DeleteOneById(id));
    }
}
