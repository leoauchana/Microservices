using Application;
using Infraestructure;
using Infraestructure.Configurations.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.Section))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddApplicationServices();
builder.Services.AddInfraestructureServices();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();