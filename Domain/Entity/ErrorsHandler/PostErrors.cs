namespace Domain.Entity.ErrorsHandler;

public static class PostErrors
{
    public static readonly Errors Nullable =
        new("Post.Nullable", "We can't find this Post now, try again later");

    public static readonly Errors NotFoundAnyPost =
        new("Post.NotFoundAny", "Something wrong when fetching list of posts");
}
