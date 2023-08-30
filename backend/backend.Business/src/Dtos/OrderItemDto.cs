using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class OrderItemReadDto
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }

}

public class OrderItemCreateDto
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
}

public class OrderItemUpdateDto
{
    public int Amount { get; set; }
}
