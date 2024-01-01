﻿using Application.Abstraction;
using Application.Mapping;
using Application.Posts.Command;
using Application.Users.Command;
using Azure.Identity;
using Blog_Api.Filter;
using Blog_Api.Services;
using Domain.Entity.User;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.Extensions;

public static class BlogApiExtension
{
    public static void RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
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
        builder.Services.AddAutoMapper(typeof(PostProfile), typeof(UserProfile));
    }

    public static void RegisterService(this WebApplicationBuilder builder)
    {
        var userConnectionString = new KeyVault().GetSecret("blogsql");
        builder.Services.AddDbContext<SocialDbContext>(
            opt => opt.UseSqlServer(userConnectionString)
        );
        builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(userConnectionString));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
    }

    public static void AddIdentityApi(this IServiceCollection service)
    {
        service.AddHttpContextAccessor();
        service
            .AddIdentity<User, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = false
            )
            .AddEntityFrameworkStores<UserDbContext>();
        service.Configure<IdentityOptions>(option =>
        {
            // Password setting
            option.Password.RequireDigit = false;
            option.Password.RequiredLength = 3;
            option.Password.RequireLowercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequireUppercase = false;

            // Lockout setting
            option.Lockout.AllowedForNewUsers = true;

            // User setting
            option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            option.User.RequireUniqueEmail = true;
        });
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

    public static void ConfigurationServices(this WebApplicationBuilder builder)
    {
        var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultURL").Value!);
        var azureCredential = new DefaultAzureCredential();
        builder.Configuration.AddAzureKeyVault(keyVaultUrl, azureCredential);
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
