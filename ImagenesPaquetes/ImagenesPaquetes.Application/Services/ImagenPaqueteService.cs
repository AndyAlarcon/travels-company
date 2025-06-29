using ImagenesPaquetes.Application.DTOs;
using ImagenesPaquetes.Domain.Entities;
using ImagenesPaquetes.Domain.Interfaces;

namespace ImagenesPaquetes.Application.Services;

public class ImagenPaqueteService : IImagenPaqueteService
{
    private readonly IImagenesPaquetesRepository _repository;
    private readonly string _rutaBase;

    public ImagenPaqueteService(IImagenesPaquetesRepository repository)
    {
        _repository = repository;
        _rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

        if (!Directory.Exists(_rutaBase))
        {
            Directory.CreateDirectory(_rutaBase);
        }
    }
    public async Task<int> GuardarImagenAsync(Stream archivo, string nombreOriginal, int idPaquete, string descripcion)
    {
        var extension = Path.GetExtension(nombreOriginal);
        var nombreArchivoUnico = $"{Guid.NewGuid()}{extension}";
        var rutaCompleta = Path.Combine(_rutaBase, nombreArchivoUnico);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        //Guardar metadatos en BBDD
        var imagen = new ImagenPaquete
        {
            PaqueteId = idPaquete,
            NombreArchivo = nombreArchivoUnico,
            Descripcion = descripcion,
            FechaDeCarga = DateTime.Now
        };

        await _repository.AddAsync(imagen);

        return imagen.Id;
    }

    public async Task<IEnumerable<ImagenPaqueteDTO>> ListarPorPaqueteAsync(int idPaquete)
    {
        var imagenes = await _repository.GetAllAsync();
        var imagenesPaquete = imagenes.Where(p => p.PaqueteId == idPaquete);

        return imagenesPaquete.Select(p => new ImagenPaqueteDTO
        {
            Id = p.Id,
            PaqueteId = p.PaqueteId,
            NombreArchivo = p.NombreArchivo,
            Descripcion = p.Descripcion,
            FechaDeCarga = p.FechaDeCarga
        });
    }
}