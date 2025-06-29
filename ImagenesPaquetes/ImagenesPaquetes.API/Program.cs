using ImagenesPaquetes.Application.Services;
using ImagenesPaquetes.Domain.Interfaces;
using ImagenesPaquetes.Infrastructure.Data;
using ImagenesPaquetes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.WebHost.UseUrls("http://0.0.0.0:80");

// 🔗 Connection String
builder.Services.AddDbContext<ImagenesPaquetesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IImagenesPaquetesRepository, ImagenesPaquetesRepository>();
builder.Services.AddScoped<IImagenPaqueteService, ImagenPaqueteService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Microservicio de Imagenes Paquetes",
        Version = "v1",
        Description = "API para gestionar imagenes de paquetes del sistema de viajes."
    });
});

var app = builder.Build();

// 📦 Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();
