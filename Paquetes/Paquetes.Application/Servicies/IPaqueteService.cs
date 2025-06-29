using Paquetes.Application.DTOs;

namespace Paquetes.Application.Services;

public interface IPaqueteService
{
    Task<PaqueteDTO> GetByIdAsync(int id);

    Task<PaqueteDTO> GetByCodeAsync(string CodigoPaquete);
    Task<IEnumerable<PaqueteDTO>> GetAllAsync();
    Task <PaqueteDTO>CreateAsync(PaqueteDTO paquete);
    Task UpdateAsync(PaqueteDTO paquete);
    Task PatchValorAsync(string CodigoPaquete, double valor);
    Task DeleteAsync(int id);
}