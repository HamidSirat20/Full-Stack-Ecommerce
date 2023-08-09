using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

public class ReviewController
    : RootController<Review, ReviewReadDto, ReviewCreateDto, ReviewUpdateDto>
{
    public ReviewController(IReviewService baseService)
        : base(baseService) { }

    [Authorize]
    public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] string id)
    {
        var objToBeDeleted = await base.DeleteOneById(id);

        if (objToBeDeleted == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [Authorize]
    public override async Task<ActionResult<ReviewReadDto>> UpdateOne(
        [FromRoute] string id,
        [FromBody] ReviewUpdateDto updateDto
    )
    {
        var updatedEntity = await base.UpdateOne(id, updateDto);
        return new AcceptedResult("Updated", updatedEntity);
    }
}
