using Application.Abstraction;
using Application.Mapping;
using Application.Posts.Command;
using Application.Users.Command;
using Azure.Identity;
using Blog_Api.Abstractions;
using Blog_Api.Filter;
using Blog_Api.Identity;
using Domain.Entity.Users;
using Domain.Enum;
using Infrastructure;
using Infrastructure.Abstraction;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.Extensions;

public static class BlogApiExtension
{
    public static void RegisterDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddScoped<ITagRepository, TagRepository>();
        builder.Services.AddScoped<IUserServiceRepository, UserServiceRepository>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreatePost.Command).Assembly);
        });
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(RegisterUser.Command).Assembly);
        });
        builder.Services.AddAutoMapper(
            typeof(PostProfile),
            typeof(UserProfile),
            typeof(CommentProfile)
        );
        builder.Services.AddSignalR();
    }

    public static void RegisterService(this WebApplicationBuilder builder)
    {
        var userConnectionString = SecretService.GetSecret($"{nameof(Secret.blogsql)}");

        builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(userConnectionString));

        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AddIdentityApi(this IServiceCollection service)
    {
        var jwtSecret = SecretService.GetSecret(nameof(Secret.jwtsecret));

        service
            .AddIdentity<User, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = false
            )
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<UserDbContext>();

        service
            .AddAuthentication(options => options.ConfigureAuthOptions())
            .AddJwtBearer(options => options.ConfigureBearerOption(jwtSecret));

        service.AddAuthorization(options => options.ConfigureAuthorization());

        service.Configure<IdentityOptions>(options =>
        {
            options.AddIdentityOptions();
        });
        service.AddCors(opt => opt.ConfigureCors());
        service.AddSwaggerGen(options => options.AddSwaggerAuth());
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
        if (!app.Environment.IsDevelopment())
            return;

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
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
