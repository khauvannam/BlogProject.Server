using BlogProject.Server.MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterService();

var app = builder.Build();
app.RegisterEndpointDefinitions();

app.UseHttpsRedirection();

app.AddSwagger();

//app.ExceptionHandler();

//app.MiddlewareHandler();


app.Run();
