using Paquetes.Application.DTOs;
using Paquetes.Domain.Entities;
using Paquetes.Domain.Interfaces;

namespace Paquetes.Application.Services;

public class PaqueteService : IPaqueteService
{
    private readonly IPaqueteRepository _repository;

    public PaqueteService(IPaqueteRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<PaqueteDTO>> GetAllAsync()
    {
        var paquetes = await _repository.GetAllAsync();
        return paquetes.Select(MapToDto);
    }
    public async Task<PaqueteDTO?> GetByIdAsync(int id)
    {
        var paquete = await _repository.GetByIdAsync(id);
        return paquete is null ? null : MapToDto(paquete);
    }
    public async Task<PaqueteDTO?> GetByCodeAsync(string CodigoPaquete)
    {
        var paquete = await _repository.GetByCodeAsync(CodigoPaquete);
        return paquete is null ? null : MapToDto(paquete);
    }
    public async Task<PaqueteDTO> CreateAsync(PaqueteDTO dto)
    {
        var Paquete = MapToEntity(dto);
        await _repository.AddAsync(Paquete);
        return MapToDto(Paquete);
    }
    public async Task UpdateAsync(PaqueteDTO dto)
    {
        var paquete = MapToEntity(dto);
        await _repository.UpdateAsync(paquete);
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
    public async Task PatchValorAsync(string codigoPaquete, double valor)
    {
        var paquete = await _repository.GetByCodeAsync(codigoPaquete);
        if (paquete is null)
            return;
        paquete.Valor = valor;
        await _repository.UpdateAsync(paquete); 
    }

    private PaqueteDTO MapToDto(Paquete paquete) => new()
    {
        Id = paquete.Id,
        CodigoPaquete = paquete.CodigoPaquete,
        NombrePaquete = paquete.NombrePaquete,
        Descripcion = paquete.Descripcion,
        Valor = paquete.Valor
    };
    private Paquete MapToEntity(PaqueteDTO dto) => new()
    {
        Id = dto.Id,
        CodigoPaquete = dto.CodigoPaquete,
        NombrePaquete = dto.NombrePaquete,
        Descripcion = dto.Descripcion,
        Valor = dto.Valor
    };
}