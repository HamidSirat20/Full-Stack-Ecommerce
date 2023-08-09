using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Interfaces;

public interface IProductService : IBaseService<Product, ProductReadDto,ProductCreateDto,ProductUpdateDto>
{

}
