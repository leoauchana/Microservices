using Data;
using Application;
using Api.Middleware;
using Transversal.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.Section))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddApplicationServices();
builder.Services.AddDataServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();