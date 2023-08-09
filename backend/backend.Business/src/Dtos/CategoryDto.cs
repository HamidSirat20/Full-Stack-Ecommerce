using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class CategoryReadDto
{
    public List<Image> ImagesUrl { get; set; }
    public string CategoryName { get; set; }
}

public class CategoryCreateDto
{
    public List<Image> ImagesUrl { get; set; }
    public string CategoryName { get; set; }
}

public class CategoryUpdateDto
{
    public List<Image> ImagesUrl { get; set; }
    public string CategoryName { get; set; }
}
