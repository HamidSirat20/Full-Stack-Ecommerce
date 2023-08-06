using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public Role Role { get; set; }
}
