using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class ReviewReadDto
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
}

public class ReviewCreateDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
}

public class ReviewUpdateDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
}
