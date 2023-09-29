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
    private readonly IImageRepo _imageRepo;

    public ProductService(
        IProductRepo ProductRepo,
        IImageRepo imageRepo,
        IMapper mapper,
        ICategoryRepo CategoryRepo
    )
        : base(ProductRepo, mapper)
    {
        _productRepo = ProductRepo;
        _categoryRepo = CategoryRepo;
        _imageRepo = imageRepo;
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

    public override async Task<ProductReadDto> CreateOne(ProductCreateDto productCreateDto)
    {
        try
        {
            var categoryName = productCreateDto.CategoryName;
            var foundCategory = await _categoryRepo.GetCategoryByName(categoryName);

            if (foundCategory == null)
            {
                throw new Exception($"Category with name '{categoryName}' not found.");
            }

            var product = new Product
            {
                Title = productCreateDto.Title,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                Inventory = productCreateDto.Inventory,
                CategoryId = foundCategory.Id,
            };
            product.Category = foundCategory;

            var createdProduct = await _productRepo.CreateOne(product);

            foreach (var item in productCreateDto.Images)
            {
                var image = new Image { ImageUrls = item.ImageUrls, ProductId = product.Id };
                image.Product = createdProduct;

                await _imageRepo.CreateOne(image);
            }

            var returnProduct = _mapper.Map<ProductReadDto>(createdProduct);
            return returnProduct;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating a product: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }

            throw;
        }
    }
}
