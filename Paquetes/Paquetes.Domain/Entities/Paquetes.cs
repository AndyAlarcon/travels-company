namespace Paquetes.Domain.Entities;

public class Paquete
{
    public int Id { get; set; }
    public string CodigoPaquete { get; set; }
    public string NombrePaquete { get; set; }
    public string Descripcion { get; set; }
    public double Valor { get; set; }

}