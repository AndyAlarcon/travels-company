using Clientes.Application.DTOs;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;

namespace Clientes.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly ICryptoService _crypto;

    public ClienteService(IClienteRepository repository, ICryptoService crypto)
    {
        _repository = repository;
        _crypto = crypto;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();
        return clientes.Select(MapToDto);
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        return cliente is null ? null : MapToDto(cliente);
    }

    public async Task CreateAsync(ClienteDto dto)
    {
        var cliente = MapToEntity(dto);
        cliente.FechaRegistro = DateTime.UtcNow;
        cliente.Activo = true;

        await _repository.AddAsync(cliente);
    }

    public async Task UpdateAsync(ClienteDto dto)
    {
        var cliente = MapToEntity(dto);
        await _repository.UpdateAsync(cliente);
    }

    public async Task PatchCorreoAsync(int id, string nuevoCorreo)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente is null)
            return;

        cliente.Correo = _crypto.Encrypt(nuevoCorreo);
        await _repository.UpdateAsync(cliente);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    // 🔁 Mapeo manual con encriptación
    private ClienteDto MapToDto(Cliente entity) => new()
    {
        Id = entity.Id,
        NumeroIdentificacion = entity.NumeroIdentificacion,
        Nombres = entity.Nombres,
        PrimerApellido = entity.PrimerApellido,
        SegundoApellido = entity.SegundoApellido,
        Telefono = entity.Telefono,
        Edad = entity.Edad,
        Direccion = entity.Direccion,
        Correo = _crypto.Decrypt(entity.Correo)
    };

    private Cliente MapToEntity(ClienteDto dto) => new()
    {
        Id = dto.Id,
        NumeroIdentificacion = dto.NumeroIdentificacion,
        Nombres = dto.Nombres,
        PrimerApellido = dto.PrimerApellido,
        SegundoApellido = dto.SegundoApellido,
        Telefono = dto.Telefono,
        Edad = dto.Edad,
        Direccion = dto.Direccion,
        Correo = _crypto.Encrypt(dto.Correo)
    };
}
