using Infraestructure;
using Application;
using Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();
app.Run();