using InmobiliariaApi.Data;
using InmobiliariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaApi.Repositories
{
    public class RepositorioPago
    {
        private readonly AppDBContext _context;

        public RepositorioPago(AppDBContext context)
        {
            _context = context;
        }

        //Obtener pagos por contrato
        public async Task<List<Pago>> GetByContratoAsync(int idContrato)
        {
            /*return await _context.Pagos
                .Include(p => p.Contrato)
                .ThenInclude(c => c.Inquilino)
                .Where(p => p.IdContrato == idContrato)
                .ToListAsync();*/
            
            /*return await _context.Pagos
            .Include(p => p.Contrato)
                .ThenInclude(c => c.Inquilino)
            .Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
            .Where(p => p.IdContrato == idContrato)
            .ToListAsync();*/
        
            return await _context.Pagos
            .Include(p => p.Contrato)
                .ThenInclude(c => c.Inquilino)
            .Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                    .ThenInclude(i => i.Duenio)
            .Where(p => p.IdContrato == idContrato)
            .ToListAsync();
        }
    }
}