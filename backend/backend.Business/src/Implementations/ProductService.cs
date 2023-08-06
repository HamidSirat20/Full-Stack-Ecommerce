using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ProductService : BaseService<Product, ProductDto>
{
    public ProductService(IProductRepo productRepo, IMapper mapper) : base(productRepo, mapper)
    {
    }
}
