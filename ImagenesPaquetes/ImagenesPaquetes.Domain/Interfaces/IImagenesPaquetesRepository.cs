using ImagenesPaquetes.Domain.Entities;

namespace ImagenesPaquetes.Domain.Interfaces;

public interface IImagenesPaquetesRepository
{
    Task<ImagenPaquete?> GetByIdAsync(int id);
    Task<IEnumerable<ImagenPaquete>> GetAllAsync();
    Task AddAsync(ImagenPaquete imagen);
    Task UpdateAsync(ImagenPaquete imagen);
    Task DeleteAsync(int id);
}