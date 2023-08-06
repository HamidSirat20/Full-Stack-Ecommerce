namespace backend.Domain.src.Entities;

public class Category : BaseEntity
{
    public List<Image> ImagesUrl { get; set; }
    public string CategoryName { get; set; }
    public ICollection<Product> Products{ get; set; }
}
