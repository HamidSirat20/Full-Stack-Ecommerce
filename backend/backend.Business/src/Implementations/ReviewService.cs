using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ReviewService
    : BaseService<Review, ReviewReadDto, ReviewCreateDto, ReviewUpdateDto>,
        IReviewService
{
    private readonly IReviewRepo _reviewRepo;
    private readonly IProductRepo _productRepo;
    private readonly IUserRepo _userRepo;

    public ReviewService(
        IReviewRepo reviewRepo,
        IMapper mapper,
        IUserRepo userRepo,
        IProductRepo productRepo
    )
        : base(reviewRepo, mapper)
    {
        _reviewRepo = reviewRepo;
        _productRepo = productRepo;
        _userRepo = userRepo;
    }

    public override async Task<ReviewReadDto> CreateOne(ReviewCreateDto newReview)
    {
        try
        {
            var productId = newReview.ProductId;
            var foundProduct = await _productRepo.GetOneById(productId);
            var userId = newReview.UserId;
            var foundUser = await _userRepo.GetOneById(userId);
            var review = _mapper.Map<Review>(newReview);
            review.User = foundUser;
            review.Product = foundProduct;
            await _reviewRepo.CreateOne(review);
            return _mapper.Map<ReviewReadDto>(review);
        }
        catch (Exception ex)
        {
            throw CustomErrorHandler.CreateEntityException(ex.Message);
        }
    }

    public async Task<ReviewReadDto> GetOneById(Guid id)
    {
        try
        {
            var entity = _mapper.Map<ReviewReadDto>(await _reviewRepo.GetOneById(id));
            if (entity is not null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"item with {id} id not found");
            }
        }
        catch (System.Exception)
        {
            throw CustomErrorHandler.NotFoundException();
        }
    }
}
