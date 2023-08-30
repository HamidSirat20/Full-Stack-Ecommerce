using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class CategoryService
    : BaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>,
        ICategoryService
{
    public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        : base(categoryRepo, mapper) { }
}
