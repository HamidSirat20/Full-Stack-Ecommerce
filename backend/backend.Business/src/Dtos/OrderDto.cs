using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class OrderDto
{
    public string ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
}
