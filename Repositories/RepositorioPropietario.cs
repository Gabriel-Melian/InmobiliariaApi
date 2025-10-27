using InmobiliariaApi.Data;
using InmobiliariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaApi.Repositories
{
    //Este repositorio se inyecta despues en los Controllers, y sera el que hable con EF Core directamente
    public class RepositorioPropietario
    {
        private readonly AppDBContext _context;

        public RepositorioPropietario(AppDBContext context)
        {
            _context = context;
        }

        //Obtener por Email
        public async Task<Propietario?> GetByEmailAsync(string email)
        {
            return await _context.Propietarios.FirstOrDefaultAsync(p => p.Email == email);
        }

        //Obtener por ID
        public async Task<Propietario?> GetByIdAsync(int id)
        {
            return await _context.Propietarios.FindAsync(id);
        }

        //Actualizar
        public async Task UpdateAsync(Propietario propietario)
        {
            _context.Propietarios.Update(propietario);
            await _context.SaveChangesAsync();
        }

        //Obtener todos (Por las dudas)
        public async Task<List<Propietario>> GetAllAsync()
        {
            return await _context.Propietarios.ToListAsync();
        }
    }
}