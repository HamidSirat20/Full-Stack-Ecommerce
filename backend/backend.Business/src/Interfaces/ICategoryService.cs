using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface ICategoryService : IBaseService<Category, CategoryReadDto,CategoryCreateDto,CategoryUpdateDto>
{

}
