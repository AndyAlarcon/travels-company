namespace Clientes.Application.DTOs;

public class ClienteDto
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
}
