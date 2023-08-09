using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>,IProductService
{
    public ProductService(IProductRepo ProductRepo, IMapper mapper) : base(ProductRepo, mapper)
    {
    }
}
