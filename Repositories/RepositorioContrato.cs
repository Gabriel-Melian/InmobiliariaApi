using InmobiliariaApi.Data;
using InmobiliariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaApi.Repositories
{
    public class RepositorioContrato
    {
        private readonly AppDBContext _context;
        public RepositorioContrato(AppDBContext context)
        {
            _context = context;
        }

        //Obtener contratos por inmueble (solo si el inmueble pertenece al propietario autenticado)
        public async Task<List<Contrato>> GetByInmuebleAsync(int idInmueble, string emailPropietario)
        {
            return await _context.Contratos
                .Include(c => c.Inquilino)
                .Include(c => c.Inmueble)
                .ThenInclude(i => i.Duenio)
                .Where(c => c.Inmueble.IdInmueble == idInmueble && c.Inmueble.Duenio.Email == emailPropietario)
                .ToListAsync();
        }
    }
}