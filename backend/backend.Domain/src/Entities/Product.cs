namespace backend.Domain.src.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
