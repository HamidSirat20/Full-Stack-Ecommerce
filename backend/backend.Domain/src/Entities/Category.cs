using System.ComponentModel.DataAnnotations;

namespace backend.Domain.src.Entities;

public class Category : BaseEntity
{
    public required string CategoryName { get; set; }

    [Url]
    public required string Image { get; set; }
    public ICollection<Product> Products { get; set; }
}
