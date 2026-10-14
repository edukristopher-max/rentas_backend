using backend.Data;
using backend.Helpers;
using backend.Interfaces;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IContribuyenteService, ContribuyenteService>();
builder.Services.AddScoped<IPredioService, PredioService>();
builder.Services.AddScoped<IImpuestoPredialService, ImpuestoPredialService>();
builder.Services.AddScoped<IArbitrioMunicipalService, ArbitrioMunicipalService>();
builder.Services.AddScoped<IDeudaService, DeudaService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<ILicenciaTasaService, LicenciaTasaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IActividadRecienteService, ActividadRecienteService>();
builder.Services.AddScoped<IConfiguracionService, ConfiguracionService>();
builder.Services.AddScoped<ICajaService, CajaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// Aplicar migraciones pendientes automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        logger.LogInformation("Verificando y aplicando migraciones automáticas en la base de datos...");
        await context.Database.MigrateAsync();
        logger.LogInformation("Base de datos sincronizada y migraciones aplicadas correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones automáticas al inicio.");
    }
}

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
