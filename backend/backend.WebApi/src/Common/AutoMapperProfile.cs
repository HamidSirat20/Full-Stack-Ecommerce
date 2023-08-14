using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.WebApi.src.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserReadDto, User>();
        CreateMap<User, UserCreateDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserUpdateDto>();
        CreateMap<UserUpdateDto, User>();

        CreateMap<Product, ProductReadDto>();
        CreateMap<ProductReadDto, Product>();
        CreateMap<Product, ProductCreateDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<Product, ProductUpdateDto>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<Order, OrderReadDto>();
        CreateMap<OrderReadDto, Order>();
        CreateMap<Order, OrderCreateDto>();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<Order, OrderUpdateDto>();
        CreateMap<OrderUpdateDto, Order>();

        CreateMap<OrderItem, OrderItemReadDto>();
        CreateMap<OrderItemReadDto, OrderItem>();
        CreateMap<OrderItem, OrderItemCreateDto>();
        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItem, OrderItemUpdateDto>();
        CreateMap<OrderItemUpdateDto, OrderItem>();
    }
}
