using Microsoft.AspNetCore.Mvc;
using Paquetes.Application.DTOs;
using Paquetes.Application.Services;

namespace Paquetes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaquetesController : ControllerBase
{
    private readonly IPaqueteService _paqueteService;

    public PaquetesController(IPaqueteService paqueteService)
    {
        _paqueteService = paqueteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var paquetes = await _paqueteService.GetAllAsync();
        return Ok(paquetes);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var paquete = await _paqueteService.GetByIdAsync(id);
        if (paquete is null)
        {
            return NotFound();
        }
        return Ok(paquete);
    }
    [HttpGet("codigoPaquete/{codigoPaquete}")]
    public async Task<IActionResult> GetByCode(string codigoPaquete)
    {
        var paquete = await _paqueteService.GetByCodeAsync(codigoPaquete);
        if (paquete is null)
        {
            return NotFound();
        }
        return Ok(paquete);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaqueteDTO dto)
    {
        var result = await _paqueteService.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PaqueteDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id no conduerda");
        await _paqueteService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpPatch("{codigoPaquete}/valor")]
    public async Task<IActionResult> PatchValor(string codigoPaquete, [FromBody] double valor)
    {
        await _paqueteService.PatchValorAsync(codigoPaquete, valor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _paqueteService.DeleteAsync(id);
        return NoContent();
    }
}