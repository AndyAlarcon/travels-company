using Microsoft.EntityFrameworkCore;
using Paquetes.Domain.Entities;

namespace Paquetes.Infrastructure.Data;

public class PaquetesDbContext : DbContext
{
    public PaquetesDbContext(DbContextOptions<PaquetesDbContext> options) : base(options)
    {

    }
    public DbSet<Paquete> Paquetes => Set<Paquete>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.ToTable("paquetes");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.CodigoPaquete).HasMaxLength(50).IsRequired();
            entity.Property(p => p.NombrePaquete).HasMaxLength(250).IsRequired();
            entity.Property(p => p.Descripcion).HasMaxLength(500).IsRequired();
            entity.Property(p => p.Valor).IsRequired();

        });
    }
}
