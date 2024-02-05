namespace Domain.Entity.ErrorsHandler;

public static class TokenErrors
{
    public static readonly Errors Invalid =
        new("Token.Invalid", "Invalid token request: Tokens invaluable");
}
