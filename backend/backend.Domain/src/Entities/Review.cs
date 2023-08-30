using System.ComponentModel.DataAnnotations;

namespace backend.Domain.src.Entities;

public class Review : BaseEntity
{
    public string Comment { get; set; }

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
