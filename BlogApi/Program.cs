using Blog_Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigurationServices();
builder.RegisterService();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityApi();

var app = builder.Build();

// app.RegisterEndpointDefinitions();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.AddSwagger();

//app.ExceptionHandler();

app.MiddlewareHandler();
app.Run();
