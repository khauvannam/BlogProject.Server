using Blog_Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigurationServices();
builder.RegisterDependencyInjection();
builder.RegisterService();

builder.Services.AddControllersWithViews();
builder.Services.AddIdentityApi();

var app = builder.Build();

// app.RegisterEndpointDefinitions();
app.UseHttpsRedirection();
app.AddSwagger();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//app.ExceptionHandler();

app.MiddlewareHandler();
app.Run();
