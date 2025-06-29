namespace ImagenesPaquetes.Domain.Entities;

public class ImagenPaquete
{
   public int Id { get; set; }
    public int PaqueteId { get; set; } // FK lógica al microservicio de Paquetes
    public string NombreArchivo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaDeCarga { get; set; }
}
