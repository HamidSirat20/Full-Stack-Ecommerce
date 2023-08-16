using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class ReviewRepo : BaseRepo<Review>, IReviewRepo
{
    public ReviewRepo(DatabaseContext dbContext)
        : base(dbContext) { }
}
