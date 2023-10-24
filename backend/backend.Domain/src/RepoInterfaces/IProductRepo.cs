using backend.Domain.src.Entities;

namespace backend.Domain.src.RepoInterfaces;

public interface IProductRepo : IBaseRepo<Product>
{
    // Task<Product> AddProductAndImageAsync(Product product, Image image);
}
