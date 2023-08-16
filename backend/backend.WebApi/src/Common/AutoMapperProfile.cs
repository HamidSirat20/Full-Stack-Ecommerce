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

        CreateMap<Image, ImageReadDto>();
        CreateMap<ImageReadDto, Image>();
        CreateMap<Image, ImageCreateDto>();
        CreateMap<ImageCreateDto, Image>();
        CreateMap<Image, ImageUpdateDto>();
        CreateMap<ImageUpdateDto, Image>();

        CreateMap<Review, ReviewReadDto>();
        CreateMap<ReviewReadDto, Review>();
        CreateMap<Review, ReviewCreateDto>();
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<Review, ReviewUpdateDto>();
        CreateMap<ReviewUpdateDto, Review>();

        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryReadDto, Category>();
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryUpdateDto>();
        CreateMap<CategoryUpdateDto, Category>();
    }
}
