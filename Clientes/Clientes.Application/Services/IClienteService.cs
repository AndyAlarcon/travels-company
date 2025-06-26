using Clientes.Application.DTOs;

namespace Clientes.Application.Services;

public interface IClienteService
{
    Task<ClienteDto?> GetByIdAsync(int id);
    Task<IEnumerable<ClienteDto>> GetAllAsync();
    Task CreateAsync(ClienteDto dto);
    Task UpdateAsync(ClienteDto dto);
    Task PatchCorreoAsync(int id, string nuevoCorreo);
    Task DeleteAsync(int id);
}
