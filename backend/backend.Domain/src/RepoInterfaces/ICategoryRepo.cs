using backend.Domain.src.Entities;

namespace backend.Domain.src.RepoInterfaces;

public interface ICategoryRepo : IBaseRepo<Category>
{
    Task<Category> GetCategoryByName(string CategoryName);
}
