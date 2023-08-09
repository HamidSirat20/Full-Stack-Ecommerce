using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

[Authorize]
public class ProductController
    : RootController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
{
    public ProductController(IProductService baseService)
        : base(baseService) { }

    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll(
        [FromQuery] QueryParameters queryParameters
    )
    {
        try
        {
            var result = await base.GetAll(queryParameters);

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
    public override async Task<ActionResult<ProductReadDto>> GetOneById([FromRoute] string id)
    {
        var foundEntity = await base.GetOneById(id);

        if (foundEntity == null)
        {
            return NotFound();
        }

        return Ok(foundEntity);
    }
}
