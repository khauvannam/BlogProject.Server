using System.Security.Claims;
using Domain.Entity.ErrorsHandler;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.Extensions;

public static class EndpointExtension
{
    public static void UseMinimalEndpoint(this WebApplication app)
    {
        app.MapGet(
            "/favourite",
            async (HttpContext context, UserDbContext dbContext) =>
            {
                var userId = context.User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));

                if (userId is null)
                {
                    return Results.BadRequest(UserErrors.NotFound);
                }
                var favouritePost = await dbContext.Posts
                    .Include(p => p.FavouritePostsList)
                    .Where(
                        p =>
                            p.FavouritePostsList != null
                            && p.FavouritePostsList.Any(fp => fp.UserId == userId)
                    )
                    .ToListAsync();
                return Results.Ok(favouritePost);
            }
        );

        app.MapGet(
            "/history",
            async (HttpContext context, UserDbContext dbContext) =>
            {
                var userId = context.User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
                if (userId is null)
                {
                    return Results.BadRequest(UserErrors.NotFound);
                }
                var historyPost = await dbContext.Posts
                    .Include(p => p.FavouritePostsList)
                    .Where(
                        p =>
                            p.FavouritePostsList != null
                            && p.FavouritePostsList.Any(fp => fp.UserId == userId)
                    )
                    .ToListAsync();
                return Results.Ok(historyPost);
            }
        );

        app.MapGet(
            "/tag/{tagName}",
            (string tagName, UserDbContext dbContext) =>
            {
                var tag = dbContext.Tags.FirstOrDefault(e => e.TagName == tagName);
                if (tag is null)
                {
                    return Results.BadRequest(TagErrors.Nullable);
                }
                var posts = dbContext.Posts
                    .Include(p => p.PostTags)
                    .Where(e => e.PostTags.Any(pt => pt.TagId == tag!.Id))
                    .ToList();
                return Results.Ok(posts);
            }
        );
    }
}
