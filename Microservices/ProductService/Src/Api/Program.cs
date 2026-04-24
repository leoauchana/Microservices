using Application;
using Data;
using Api.Middleware;
using Transversal.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add options patterns.
builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.Section))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddDataServices();
builder.Services.AddControllers();

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();
app.Run();