namespace Clientes.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string NumeroIdentificacion { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string PrimerApellido { get; set; } = null!;
    public string SegundoApellido { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public int Edad { get; set; }
    public string Direccion { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public bool Activo { get; set; } = true;
}
