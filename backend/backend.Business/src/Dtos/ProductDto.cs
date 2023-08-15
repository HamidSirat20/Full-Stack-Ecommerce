using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class ProductReadDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
}

public class ProductCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}

public class ProductUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}
