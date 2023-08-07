using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User,UserDto>();
        CreateMap<UserDto,User>();
        CreateMap<ProductDto,Product>();
        CreateMap<Product,ProductDto>();
    }
}
