using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

[Authorize(Roles ="Admin")]
public class CategorieController
    : RootController<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
{
    private readonly ICategoryService _categoryService;

    public CategorieController(ICategoryService categoryService)
        : base(categoryService)
    {
        _categoryService = categoryService;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        try
        {
            var result = await _categoryService.GetAll(queryParameters);

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

    [AllowAnonymous]
    public override async Task<ActionResult<CategoryReadDto>> GetOneById([FromRoute] Guid id)
    {
        var foundEntity = await _categoryService.GetOneById(id);

        if (foundEntity == null)
        {
            return NotFound();
        }

        return Ok(foundEntity);
    }
}
