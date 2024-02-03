namespace Blog_Api.Filter;

public class ApiEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (Exception ex)
        {
            return ex switch
            {
                ConflictException => HandlerConflictException(ex),
                _ => HandlerUnknownException(ex)
            };
        }
    }

    private static IResult HandlerConflictException(Exception exception)
    {
        var detail = new
        {
            Status = StatusCodes.Status409Conflict,
            Title = $"Conflict: {exception.Message}",
            Type = exception.GetType().ToString()
        };
        return Results.Conflict(detail);
    }

    private static IResult HandlerUnknownException(Exception exception)
    {
        var detail = new
        {
            Title = "An errors occur while processing your progress",
            Type = exception.GetType().ToString(),
            Status = StatusCodes.Status500InternalServerError
        };
        return Results.Problem(title: detail.Title, type: detail.Type, statusCode: detail.Status);
    }
}