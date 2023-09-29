using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;
using backend.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.WebApi.src.RepoImplementations;

public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
{
    private readonly DbSet<Category> _dbSet;
    private readonly DatabaseContext _context;

    public CategoryRepo(DatabaseContext dbContext)
        : base(dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Categories;
    }

    public async Task<Category?> GetCategoryByName(string CategoryName)
    {
        return await _dbSet.SingleOrDefaultAsync(category => category.CategoryName == CategoryName);
    }
}
