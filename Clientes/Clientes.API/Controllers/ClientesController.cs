using Clientes.Application.DTOs;
using Clientes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente is null)
            return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteDto dto)
    {
        await _clienteService.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch");
        await _clienteService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpPatch("{id}/correo")]
    public async Task<IActionResult> PatchCorreo(int id, [FromBody] string nuevoCorreo)
    {
        await _clienteService.PatchCorreoAsync(id, nuevoCorreo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _clienteService.DeleteAsync(id);
        return NoContent();
    }
}
