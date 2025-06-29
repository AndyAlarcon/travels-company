using Paquetes.Domain.Entities;

namespace Paquetes.Domain.Interfaces;

public interface IPaqueteRepository
{
    Task<Paquete?> GetByIdAsync(int id);
    Task<Paquete?> GetByCodeAsync(string CodigoPaquete);
    Task<IEnumerable<Paquete>> GetAllAsync();
    Task AddAsync(Paquete paquete);
    Task UpdateAsync(Paquete paquete);
    Task DeleteAsync(int id);
}