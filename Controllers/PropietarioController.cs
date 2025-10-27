using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaApi.Repositories;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]//Accede unicamente con el Token
    public class PropietarioController : ControllerBase
    {
        private readonly RepositorioPropietario _repo;

        public PropietarioController(RepositorioPropietario repo)
        {
            _repo = repo;
        }

        //Obtener perfil del propietario autenticado
        [HttpGet("perfil")]
        public async Task<IActionResult> GetPerfil()
        {
            //Obtener email del token JWT
            var email = User.Identity?.Name;
            if (email == null) return Unauthorized("Token inválido o expirado.");

            var propietario = await _repo.GetByEmailAsync(email);
            if (propietario == null) return NotFound("Propietario no encontrado.");

            return Ok(propietario);
        }
    }
}