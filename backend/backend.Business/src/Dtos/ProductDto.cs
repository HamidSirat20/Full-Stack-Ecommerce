using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class ProductReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public List<string> Images { get; set; }
    public CategoryReadDto Category { get; set; }
    public List<ReviewReadDto> Reviews { get; set; }
}

public class ProductCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public List<string> Images { get; set; }
    public string? CategoryName { get; set; }
}

public class ProductUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public Guid CategoryId { get; set; }
    public List<string> Images { get; set; }
}
