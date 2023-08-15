using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ImageService : BaseService<Image, ImageReadDto, ImageCreateDto, ImageUpdateDto>, IImageService
{
    public ImageService(IImageRepo baseRepo, IMapper mapper) : base(baseRepo, mapper)
    {
    }
}
