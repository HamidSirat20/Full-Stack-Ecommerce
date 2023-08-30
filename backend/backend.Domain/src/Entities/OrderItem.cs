using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Domain.src.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductId))]
public class OrderItem : Timestamp
{
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}
