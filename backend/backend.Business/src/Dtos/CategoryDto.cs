using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class CategoryDto
{
    public List<Image> ImagesUrl { get; set; }
    public string CategoryName { get; set; }
}
