using Blog_Api.Extensions;
using Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigurationServices();
builder.RegisterDependencyInjection();
builder.RegisterService();

builder.Services.AddControllersWithViews();
builder.Services.AddIdentityApi();

var app = builder.Build();

app.MiddlewareHandler();

// app.RegisterEndpointDefinitions();
app.UseHttpsRedirection();
app.AddSwagger();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<CommentHub>("/commentshub");
app.UseMinimalEndpoint();

//app.ExceptionHandler();

app.Run();
