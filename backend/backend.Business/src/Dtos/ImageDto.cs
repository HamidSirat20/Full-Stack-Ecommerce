namespace backend.Business.src.Dtos;

public class ImageReadDto
{
    public Guid Id { get; set; }
    public string ImageUrls { get; set; }
    public Guid ProductId { get; set; }
}

public class ImageCreateDto
{
    public string ImageUrls { get; set; }
    public Guid ProductId { get; set; }
}

public class ImageUpdateDto
{
    public string ImageUrls { get; set; }
}
