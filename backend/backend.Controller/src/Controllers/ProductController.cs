using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controller.src.Controllers;

public class ProductController
    : RootController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService baseService, IMapper mapper)
        : base(baseService)
    {
        _productService = baseService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        try
        {
            var result = await _productService.GetAll(queryParameters);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [AllowAnonymous]
    public override async Task<ActionResult<ProductReadDto>> GetOneById([FromRoute] Guid id)
    {
        var foundEntity = await _productService.GetOneById(id);

        if (foundEntity == null)
        {
            return NotFound();
        }

        return Ok(foundEntity);
    }

    // [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ProductReadDto>> CreateOne(
        [FromBody] ProductCreateDto createDto
    )
    {
        try
        {
            var createdObject = await _productService.CreateOne(createDto);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException;
            return StatusCode(
                500,
                " An error occurred while saving the entity changes." + innerException
            );
        }
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ProductReadDto>> UpdateOne(
        [FromRoute] Guid id,
        [FromBody] ProductUpdateDto updateDto
    )
    {
        return await _productService.UpdateOneById(id, updateDto);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
    {
        return StatusCode(204, await _productService.DeleteOneById(id));
    }
}
