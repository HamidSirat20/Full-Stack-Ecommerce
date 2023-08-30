namespace backend.Business.src.Dtos;

public class CategoryReadDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public string Image { get; set; }
}

public class CategoryCreateDto
{
    public string CategoryName { get; set; }
    public string Image { get; set; }
}

public class CategoryUpdateDto
{
    public string CategoryName { get; set; }
    public string Image { get; set; }
}
