using Application;
using Transversal.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.Section))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();