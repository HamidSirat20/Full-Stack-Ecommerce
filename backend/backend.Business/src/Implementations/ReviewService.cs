using AutoMapper;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ReviewService : BaseService<Review, ReviewReadDto,ReviewCreateDto,ReviewUpdateDto>,IReviewService
{
    public ReviewService(IReviewRepo reviewRepo, IMapper mapper) : base(reviewRepo, mapper)
    {
    }
}
