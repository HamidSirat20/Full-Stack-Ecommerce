using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;

namespace backend.Controller.src.Controllers;

public class ImageController : RootController<Image, ImageReadDto, ImageCreateDto, ImageUpdateDto>
{
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    public ImageController(IImageService baseService,IMapper mapper) : base(baseService)
    {
        _imageService = baseService;
        _mapper = mapper;
    }
}
