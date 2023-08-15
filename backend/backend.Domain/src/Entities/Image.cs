namespace backend.Domain.src.Entities;

public class Image : BaseEntity
{
    public string ImageUrls { get; set; }
    public string ProudctId { get; set; }
    public Product product { get; set; }
}
