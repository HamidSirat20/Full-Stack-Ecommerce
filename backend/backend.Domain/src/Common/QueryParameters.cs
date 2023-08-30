namespace backend.Domain.src.Common;

public class QueryParameters
{
    public string Search { get; set; } = String.Empty;
    public string OrderBy { get; set; } = "ModifiedAt";
    public bool OrderByDescending { get; set; } = false;
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 10;
}
