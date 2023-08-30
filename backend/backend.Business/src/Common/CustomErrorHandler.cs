namespace backend.Business.src.Common;

public class CustomErrorHandler : Exception
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public CustomErrorHandler(int statusCode = 500, string message = "Internal server error")
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }

    public static CustomErrorHandler NotFoundException(string message = "Item not found with the given Id")
    {
        throw new CustomErrorHandler(500, message);
    }

    public static CustomErrorHandler CreateEntityException(
        string message = "An error occurred while creating the entity."
    )
    {
        throw new CustomErrorHandler(400, message);
    }
}
