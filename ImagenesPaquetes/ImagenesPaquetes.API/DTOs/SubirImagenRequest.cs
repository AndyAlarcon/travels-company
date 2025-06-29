namespace ImagenesPaquetes.API.DTOs;

public class SubirImagenRequest
{
    public IFormFile Archivo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
}