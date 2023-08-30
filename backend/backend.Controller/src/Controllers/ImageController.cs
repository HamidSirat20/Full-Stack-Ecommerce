using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers;

[Authorize(Roles ="Admin")]
public class ImageController : RootController<Image, ImageReadDto, ImageCreateDto, ImageUpdateDto>
{
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public ImageController(IImageService baseService, IMapper mapper)
        : base(baseService)
    {
        _imageService = baseService;
        _mapper = mapper;
    }

    [HttpPost]
    public override async Task<ActionResult<ImageReadDto>> CreateOne([FromBody] ImageCreateDto createDto)
    {
        var createdObject = await _imageService.CreateOne(createDto);
        return CreatedAtAction(nameof(CreateOne), createdObject);
    }
}
