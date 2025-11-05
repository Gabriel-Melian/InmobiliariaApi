using InmobiliariaApi.Data;
using InmobiliariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaApi.Repositories
{
    public class RepositorioInmueble
    {
        private readonly AppDBContext _context;
        public RepositorioInmueble(AppDBContext context)
        {
            _context = context;
        }

        //Obtener todos los inmuebles de un propietario
        public async Task<List<Inmueble>> GetByPropietarioEmailAsync(string email)
        {
            return await _context.Inmuebles
                .Include(i => i.Duenio)
                .Where(i => i.Duenio.Email == email)
                .ToListAsync();
        }

        //Obtener Id del propietario por email
        public async Task<int> GetPropietarioIdByEmail(string email)
        {
            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p => p.Email == email);
            if (propietario == null)
                throw new Exception("Propietario no encontrado.");
            return propietario.IdPropietario;
        }

        public async Task<Propietario?> GetPropietarioByEmail(string email)
        {
            return await _context.Propietarios.FirstOrDefaultAsync(p => p.Email == email);
        }

        //Crear
        public async Task CreateAsync(Inmueble inmueble)
        {
            _context.Inmuebles.Add(inmueble);
            await _context.SaveChangesAsync();
        }

        //Obtener inmueble por ID y email de propietario (Solo modifico mis inmuebles)
        public async Task<Inmueble?> GetByIdAndPropietarioEmailAsync(int idInmueble, string email)
        {
            return await _context.Inmuebles
                .Include(i => i.Duenio)
                .FirstOrDefaultAsync(i => i.IdInmueble == idInmueble && i.Duenio.Email == email);
        }
        //Actualizar inmueble
        public async Task UpdateAsync(Inmueble inmueble)
        {
            _context.Inmuebles.Update(inmueble);
            await _context.SaveChangesAsync();
        }

        //Obtener inmuebles con contrato vigente (solo del propietario autenticado)
        public async Task<List<Inmueble>> GetConContratoVigenteByEmailAsync(string email)
        {
            return await _context.Inmuebles
                .Include(i => i.Duenio)
                .Where(i => i.Duenio.Email == email && i.TieneContratoVigente == true)
                .ToListAsync();
        }
    }
}