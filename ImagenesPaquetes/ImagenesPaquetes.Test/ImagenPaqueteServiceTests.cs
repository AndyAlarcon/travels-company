using ImagenesPaquetes.Application.Services;
using ImagenesPaquetes.Domain.Interfaces;
using System.Text;
using Moq;

namespace ImagenesPaquetes.Test;

public class ImagenesPaquetesTest
{
    [Fact]
    public async Task GuardarImagenAsync_Should_Call_PaqueteExisteAsync()
    {
        // Arrange
        var mockRepo = new Mock<IImagenesPaquetesRepository>();
        var mockProxy = new Mock<IPaqueteProxyService>();
        var idPaquete = 123;

        // Configura el proxy para devolver true (como si el paquete existiera)
        mockProxy.Setup(p => p.PaqueteExisteAsync(idPaquete)).ReturnsAsync(true);

        var rutaBase = Path.GetTempPath(); // Carpeta temporal de sistema
        var service = new ImagenPaqueteService(mockRepo.Object, mockProxy.Object);

        // Simula un archivo en memoria
        var contenido = new MemoryStream(Encoding.UTF8.GetBytes("contenido de prueba"));
        var nombreOriginal = "foto.jpg";
        var descripcion = "Imagen de prueba";

        // Act
        await service.GuardarImagenAsync(contenido, nombreOriginal, idPaquete, descripcion);

        // Assert: verifica que el método fue llamado
        mockProxy.Verify(p => p.PaqueteExisteAsync(idPaquete), Times.Once);
    }

}