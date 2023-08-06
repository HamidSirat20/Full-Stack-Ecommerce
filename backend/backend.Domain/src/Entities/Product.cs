namespace backend.Domain.src.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public List<Image> ImagesUrl { get; set; }
    public Category Category{ get; set; }
    public ICollection<Review> Reviews{ get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
