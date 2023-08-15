using System.Text.Json.Serialization;

namespace backend.Domain.src.Entities;

public class Order : BaseEntity
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
