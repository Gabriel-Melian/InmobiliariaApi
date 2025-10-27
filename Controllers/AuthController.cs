using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InmobiliariaApi.Data;
using InmobiliariaApi.Models;
using InmobiliariaApi.Services;
using InmobiliariaApi.Repositories;
using BCrypt.Net;

namespace InmobiliariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDBContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Propietario model)
        {
            //Verificar si ya existe el email
            if (await _context.Propietarios.AnyAsync(p => p.Email == model.Email))
                return BadRequest("El email ya está registrado.");

            //Encriptar clave
            model.Clave = BCrypt.Net.BCrypt.HashPassword(model.Clave);

            _context.Propietarios.Add(model);
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginView model)
        {
            var propietario = await _context.Propietarios
                .FirstOrDefaultAsync(p => p.Email == model.Email);

            if (propietario == null || !BCrypt.Net.BCrypt.Verify(model.Clave, propietario.Clave))
                return Unauthorized("Email o clave incorrectos.");

            var token = _jwtService.GenerateToken(propietario);
            return Ok(new { token });
        }
    }
}