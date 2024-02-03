using static System.String;

namespace Domain.Entity.ErrorsHandler;

public sealed record Errors(string Code, string Description)
{
    public static readonly Errors None = new(Empty, Empty);
}
