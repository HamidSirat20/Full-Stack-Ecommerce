using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class ReviewService : BaseService<Review, ReviewDto>
{
    public ReviewService(IReviewRepo reviewRepo, IMapper mapper) : base(reviewRepo, mapper)
    {
    }
}
