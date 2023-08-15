using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class OrderReadDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> orderItems{ get; set; }
}

public class OrderCreateDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItem> orderItems{ get; set; }

}

public class OrderUpdateDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
}
