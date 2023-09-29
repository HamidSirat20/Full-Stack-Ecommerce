using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface IImageService : IBaseService<Image, ImageReadDto, ImageCreateDto, ImageUpdateDto>
{

}
