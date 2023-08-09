namespace backend.Domain.src.Entities;
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set;}
    public string Email { get; set; }
    public string Password { get; set; }
    public byte [] Salt { get; set; }
    public string Avatar { get; set; }
    public Role Role { get; set; }
    public ICollection<Review> Reviews{ get; set; }
    public ICollection<Order> Orders { get; set; }
}

public enum Role
{
    Client,
    Admin
}
