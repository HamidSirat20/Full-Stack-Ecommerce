using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Common;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ProductService
    : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>,
        IProductService
{
    private readonly ICategoryRepo _categoryRepo;
    private readonly IProductRepo _productRepo;

    public ProductService(IProductRepo ProductRepo, IMapper mapper, ICategoryRepo CategoryRepo)
        : base(ProductRepo, mapper)
    {
        _productRepo = ProductRepo;
        _categoryRepo = CategoryRepo;
    }

    public override async Task<ProductReadDto> UpdateOneById(Guid id, ProductUpdateDto newProduct)
    {
        try
        {
            var foundItem = await _productRepo.GetOneById(id);
            if (foundItem == null)
            {
                throw new Exception($"Item with {id} id not found");
            }
            var newfound = _mapper.Map(newProduct, foundItem);
            await _productRepo.UpdateOneById(newfound);
            return _mapper.Map<ProductReadDto>(newfound);
        }
        catch (Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }

    public override async Task<IEnumerable<ProductReadDto>> GetAll(QueryParameters queryParameters)
    {
        try
        {
            var entities = await _productRepo.GetAll(queryParameters);
            var dtoEntities = _mapper.Map<IEnumerable<ProductReadDto>>(entities);

            if (dtoEntities.Any())
            {
                return dtoEntities;
            }
            else
            {
                throw CustomErrorHandler.NotFoundException("There is no item in the Repo");
            }
        }
        catch (Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }
}
