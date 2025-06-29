using ImagenesPaquetes.API.DTOs;
using ImagenesPaquetes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImagenesPaquetes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagenesPaquetesController : ControllerBase
{
    private readonly IImagenPaqueteService _service;
    private readonly ILogger<ImagenesPaquetesController> _logger;

    public ImagenesPaquetesController(IImagenPaqueteService service, ILogger<ImagenesPaquetesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("{paqueteId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SubirImagen(int paqueteId, [FromForm] SubirImagenRequest request)
    {
        if (request.Archivo is null || request.Archivo.Length == 0)
        {
            return BadRequest("El archivo no puede estar vacío.");
        }
        var stream = request.Archivo.OpenReadStream();
        var id = await _service.GuardarImagenAsync(stream, request.Archivo.FileName, paqueteId, request.Descripcion);
        if (id == 0)
        {
            return BadRequest(new{ mensaje = "El paquete no existe"});
        }
        else
        {
            return CreatedAtAction(nameof(ObtenerPorPaquete), new { paqueteId }, new { id });    
        }
    }

    [HttpGet("{paqueteId}")]
    public async Task<IActionResult> ObtenerPorPaquete(int paqueteId)
    {
        var imagenes = await _service.ListarPorPaqueteAsync(paqueteId);
        return Ok(imagenes);
    }

    [HttpGet("archivo/{nombreArchivo}")]
    public IActionResult ObtenerImagen(string nombreArchivo)
    {
        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", nombreArchivo);

        if (!System.IO.File.Exists(ruta))
            return NotFound("Archivo no encontrado.");

        var contentType = GetContentType(ruta);
        var archivoBytes = System.IO.File.ReadAllBytes(ruta);

        return File(archivoBytes, contentType, nombreArchivo);
    }

    private string GetContentType(string path)
{
    var types = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    {
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".png", "image/png"},
        {".gif", "image/gif"},
        {".bmp", "image/bmp"}
    };

    var ext = Path.GetExtension(path);
    return types.TryGetValue(ext, out var contentType) ? contentType : "application/octet-stream";
}
}