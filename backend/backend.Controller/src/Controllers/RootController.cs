using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            var result = await _baseService.GetAll(queryParameters);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TReadDto>> GetOneById([FromRoute] string id)
    {
        var foundEntity = await _baseService.GetOneById(id);

        if (foundEntity == null)
        {
            return NotFound();
        }

        return Ok(foundEntity);
    }

    [HttpPost]
    public virtual  async Task<ActionResult<TReadDto>> CreateOne([FromBody] TCreateDto createDto)
    {
        try
        {
            var createdItem = await _baseService.CreateOne(createDto);
            return CreatedAtAction("Created", createdItem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpPatch("{id}")]
    public virtual async Task<ActionResult<TReadDto>> UpdateOne(
        [FromRoute] string id,
        [FromBody] TUpdateDto updateDto
    )
    {
        var updatedEntity = await _baseService.UpdateOneById(id, updateDto);
        return new AcceptedResult("Updated", updatedEntity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<bool>> DeleteOneById([FromRoute] string id)
    {
        var objToBeDeleted = await _baseService.DeleteOneById(id);

        if (objToBeDeleted == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
