using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

public class ReviewController
    : RootController<Review, ReviewReadDto, ReviewCreateDto, ReviewUpdateDto>
{
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;

    public ReviewController(IReviewService baseService, IMapper mapper)
        : base(baseService)
    {
        _reviewService = baseService;
        _mapper = mapper;
    }

    [Authorize]
    public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
    {
        var objToBeDeleted = await _reviewService.DeleteOneById(id);

        if (objToBeDeleted == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [Authorize]
    public override async Task<ActionResult<ReviewReadDto>> UpdateOne(
        [FromRoute] Guid id,
        [FromBody] ReviewUpdateDto updateDto
    )
    {
        var updatedEntity = await _reviewService.UpdateOneById(id, updateDto);
        return new AcceptedResult(nameof(UpdateOne), updatedEntity);
    }
}
