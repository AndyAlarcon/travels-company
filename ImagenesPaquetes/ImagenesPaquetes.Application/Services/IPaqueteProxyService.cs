namespace ImagenesPaquetes.Application.Services;

public interface IPaqueteProxyService
{
    Task<bool> PaqueteExisteAsync(int PaqueteId);
}