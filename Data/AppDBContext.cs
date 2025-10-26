using Microsoft.EntityFrameworkCore;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Propietario> Propietarios { get; set; }
    }
}

/*
Lo siguiente me generaria las tablas con sus respectivos campos en la BDD
IMPORTANTE: Hacerlo con Laragon corriendo y la base de datos levantada
dotnet tool install --global dotnet-ef   -> Solo la primera vez para instalar Entity Framework
dotnet ef migrations add InitialCreate
dotnet ef database update
*/