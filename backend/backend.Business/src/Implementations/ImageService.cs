using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ImageService
    : BaseService<Image, ImageReadDto, ImageCreateDto, ImageUpdateDto>,
        IImageService
{
    private readonly IImageRepo _imageRepo;
    private readonly IProductRepo _productRepo;

    public ImageService(IImageRepo baseRepo, IMapper mapper, IProductRepo productRepo)
        : base(baseRepo, mapper)
    {
        _imageRepo = baseRepo;
        _productRepo = productRepo;
    }

    public override async Task<ImageReadDto> CreateOne(ImageCreateDto imagesDto)
    {
        try
        {
            var productId = imagesDto.ProductId;
            var foundProduct = await _productRepo.GetOneById(productId);
            var image = _mapper.Map<Image>(imagesDto);
            image.Product =foundProduct;
            await _imageRepo.CreateOne(image);
            return _mapper.Map<ImageReadDto>(image);
        }
        catch (Exception ex)
        {
            throw CustomErrorHandler.CreateEntityException(ex.Message);
        }
    }
}
