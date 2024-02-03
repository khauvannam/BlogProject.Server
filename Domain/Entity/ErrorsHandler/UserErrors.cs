namespace Domain.Entity.ErrorsHandler;

public static class UserErrors
{
    public static readonly Errors NotFound =
        new("User.NotFound", "Some problem when getting your UserId, try again.");

    public static readonly Errors Duplicate =
        new("User.Duplicate", "This username or password have been used.");

    public static readonly Errors Unauthenticated =
        new("User.Unauthenticated", "Invalid Authentication, check your email or password again");
}
