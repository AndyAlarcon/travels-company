using Microsoft.EntityFrameworkCore;
using Paquetes.Domain.Entities;
using Paquetes.Domain.Interfaces;
using Paquetes.Infrastructure.Data;

namespace Paquetes.Infrastructure.Repositories;

public class PaqueteRepository : IPaqueteRepository
{
    private readonly PaquetesDbContext _context;

    public PaqueteRepository(PaquetesDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Paquete paquete)
    {
        await _context.Paquetes.AddAsync(paquete);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Paquetes.FindAsync(id);
        if (entity is not null)
        {
            _context.Paquetes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Paquete>> GetAllAsync()
    {
        return await _context.Paquetes.ToListAsync();
    }

    public async Task<Paquete?> GetByIdAsync(int id)
    {
        return await _context.Paquetes.FindAsync(id);
    }
    public async Task<Paquete?> GetByCodeAsync(string codigoPaquete)
    {
        return await _context.Paquetes.FirstOrDefaultAsync(p => p.CodigoPaquete == codigoPaquete);
    }
    public async Task UpdateAsync(Paquete paquete)
    {
        _context.Paquetes.Update(paquete);
        await _context.SaveChangesAsync();
    }
}