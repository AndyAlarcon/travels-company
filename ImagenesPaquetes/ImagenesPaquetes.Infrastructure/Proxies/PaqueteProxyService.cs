using ImagenesPaquetes.Application.Services;
using ImagenesPaquetes.Application.Utils;
using Microsoft.Extensions.Options;

namespace ImagenesPaquetes.Infrastructure.Proxies;

public class PaqueteProxyService : IPaqueteProxyService
{
    private readonly HttpClient _httpClient;
    private readonly string _paqueteBaseURL;

    public PaqueteProxyService(HttpClient httpClient, IOptions<MicroserviceUrls> options)
    {
        _httpClient = httpClient;
        _paqueteBaseURL = options.Value.Paquetes;
    }
    public async Task<bool> PaqueteExisteAsync(int PaqueteId)
    {
        var response = await _httpClient.GetAsync($"{_paqueteBaseURL}/api/paquetes/{PaqueteId}");
        return response.IsSuccessStatusCode;
    }
}