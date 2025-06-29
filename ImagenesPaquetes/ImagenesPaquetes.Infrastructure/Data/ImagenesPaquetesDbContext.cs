using System.Dynamic;
using ImagenesPaquetes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImagenesPaquetes.Infrastructure.Data;

public class ImagenesPaquetesDbContext : DbContext
{
    public ImagenesPaquetesDbContext(DbContextOptions<ImagenesPaquetesDbContext> options) : base(options)
    {

    }
    public DbSet<ImagenPaquete> ImagenesPaquete => Set<ImagenPaquete>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagenPaquete>(entity =>
        {
            entity.ToTable("imagenes_paquetes");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.PaqueteId).IsRequired();
            entity.Property(e => e.NombreArchivo).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaDeCarga).IsRequired();
        });
    }
}