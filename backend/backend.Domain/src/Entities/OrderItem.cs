namespace backend.Domain.src.Entities;

public class OrderItem : Timestamp
{
    public int Amount { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}
