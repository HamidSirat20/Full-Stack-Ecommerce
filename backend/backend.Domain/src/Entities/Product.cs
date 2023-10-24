namespace backend.Domain.src.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public List<string> Images { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Review> Reviews { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
