using ImagenesPaquetes.Application.DTOs;

namespace ImagenesPaquetes.Application.Services;
public interface IImagenPaqueteService
{
    Task<int> GuardarImagenAsync(Stream archivo, string nombreOriginal, int idPaquete, string descripcion);
    Task<IEnumerable<ImagenPaqueteDTO>> ListarPorPaqueteAsync(int idPaquete);
}
