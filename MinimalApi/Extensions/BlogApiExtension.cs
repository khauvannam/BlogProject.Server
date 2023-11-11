using Application.Abstraction;
using Application.Posts.Command;
using Application.Users.Command;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Filter;
using MinimalApi.Services;

namespace MinimalApi.Extensions;

public static class BlogApiExtension
{
    public static void RegisterService(this WebApplicationBuilder builder)
    {
        var userConnectionString = builder.Configuration.GetConnectionString("Users");
        builder.Services.AddDbContext<SocialDbContext>(
            opt => opt.UseSqlServer(userConnectionString)
        );
        builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(userConnectionString));
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreatePost).Assembly);
        });
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Register).Assembly);
        });
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
    }

    #region exception handler

    public static void ExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(
            exception =>
                exception.Run(async context =>
                {
                    var exceptionHandlerFeaturePath =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                    var err = exceptionHandlerFeaturePath?.Error;
                    if (err is ConflictException)
                    {
                        await Results.Conflict().ExecuteAsync(context);
                        return;
                    }

                    await Results.Problem().ExecuteAsync(context);
                })
        );
    }

    #endregion

    #region middleware

    public static void MiddlewareHandler(this WebApplication app)
    {
        app.Use(
            async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    if (ex is ConflictException)
                        await Results.Conflict().ExecuteAsync(context);

                    throw;
                }
            }
        );
    }

    #endregion

    public static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    #region endpoint

    /*  public static void RegisterEndpointDefinitions(this WebApplication app)
      {
          var endPointDefinition = typeof(Program).Assembly.GetTypes()
              .Where(t => t.IsAssignableTo(typeof(IEndPointDefinitions)) &&
                          t is { IsAbstract: false, IsInterface: false })
              .Select(Activator.CreateInstance).Cast<IEndPointDefinitions>();
          foreach (var endpointDef in endPointDefinition)
          {
              endpointDef.RegisterEndpoint(app);
          }
      }
*/

    #endregion
}
