namespace backend.Domain.src.Entities;

public class Review : BaseEntity
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public Product Product{ get; set; }
    public User User { get; set; }
}
