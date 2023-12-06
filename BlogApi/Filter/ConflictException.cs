namespace Blog_Api.Filter;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
    }
}