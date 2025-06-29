namespace ImagenesPaquetes.Application.DTOs;

public class ImagenPaqueteDTO
{
     public int Id { get; set; }
    public int PaqueteId { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaDeCarga { get; set; }
}