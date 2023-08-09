namespace backend.Business.src.Dtos;

public class ReviewReadDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
}

public class ReviewCreateDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
}

public class ReviewUpdateDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
}