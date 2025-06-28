using Clientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infrastructure.Data;

public class ClientesDbContext : DbContext
{
    public ClientesDbContext(DbContextOptions<ClientesDbContext> options)
        : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroIdentificacion).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Nombres).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PrimerApellido).HasMaxLength(50).IsRequired();
            entity.Property(e => e.SegundoApellido).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Correo).HasMaxLength(250).IsRequired();
        });
    }
}
