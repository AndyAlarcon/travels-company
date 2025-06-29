using ImagenesPaquetes.Domain.Entities;
using ImagenesPaquetes.Domain.Interfaces;
using ImagenesPaquetes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImagenesPaquetes.Infrastructure.Repositories;

public class ImagenesPaquetesRepository : IImagenesPaquetesRepository
{
    private readonly ImagenesPaquetesDbContext _context;

    public ImagenesPaquetesRepository(ImagenesPaquetesDbContext context)
    {
        _context = context;
    }
    public async Task<ImagenPaquete?> GetByIdAsync(int id)
    {
        return await _context.ImagenesPaquete.FindAsync(id);
    }
    public async Task<IEnumerable<ImagenPaquete>> GetAllAsync()
    {
        return await _context.ImagenesPaquete.ToListAsync();
    }
    public async Task AddAsync(ImagenPaquete imagen)
    {
        await _context.ImagenesPaquete.AddAsync(imagen);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(ImagenPaquete imagen)
    {
        _context.ImagenesPaquete.Update(imagen);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var imagen = await _context.ImagenesPaquete.FindAsync(id);
        if (imagen is not null)
        {
            _context.ImagenesPaquete.Remove(imagen);
            await _context.SaveChangesAsync();
        }
    }
}