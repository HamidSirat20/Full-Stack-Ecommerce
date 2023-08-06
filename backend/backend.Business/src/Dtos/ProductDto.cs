using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class ProductDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public List<Image> ImagesUrl{ get; set; }
}
