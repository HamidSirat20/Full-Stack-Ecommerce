using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public Guid UserId { get; set; }
    public UserReadDto User { get; set; }
    public List<OrderItemCreateDto> orderItems { get; set; }
}

public class OrderCreateDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemCreateDto> OrderProducts { get; set; } = new List<OrderItemCreateDto>();
}

public class OrderUpdateDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemUpdateDto> OrderProducts { get; set; }
}
