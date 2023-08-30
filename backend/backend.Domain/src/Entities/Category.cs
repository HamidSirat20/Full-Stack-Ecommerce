using System.ComponentModel.DataAnnotations;

namespace backend.Domain.src.Entities;

public class Category : BaseEntity
{
    public string CategoryName { get; set; }
    public string Image { get; set; }
    public List<Product> Products { get; set; }
}
