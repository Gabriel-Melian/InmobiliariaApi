using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaApi.Repositories;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContratoController : ControllerBase
    {
        private readonly RepositorioContrato _repo;

        public ContratoController(RepositorioContrato repoContrato)
        {
            _repo = repoContrato;
        }

        //GET: api/contrato/inmueble/1
        [HttpGet("inmueble/{idInmueble}")]
        public async Task<IActionResult> GetContratosPorInmueble(int idInmueble)
        {
            try
            {
                //Obtener email del token
                var email = User.Identity?.Name;
                if (email == null) return Unauthorized("Token inválido o expirado.");

                var contratos = await _repo.GetByInmuebleAsync(idInmueble, email);

                if (contratos == null || !contratos.Any())
                    return NotFound("No se encontraron contratos para este inmueble.");

                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener contratos: {ex.Message}");
            }
        }
    }
}