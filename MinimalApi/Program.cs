using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterService();
builder.Services.AddControllersWithViews();

var app = builder.Build();
// app.RegisterEndpointDefinitions();

app.UseHttpsRedirection();
app.MapControllers();
app.AddSwagger();

//app.ExceptionHandler();

app.MiddlewareHandler();


app.Run();