namespace Domain.Entity.ErrorsHandler;

public class TagErrors
{
    public static readonly Errors Nullable =
        new("Tag.Nullable", "Something wrong, try different tags");
}
