using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaApi.Repositories;

namespace InmobiliariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PagoController : ControllerBase
    {
        private readonly RepositorioPago _repo;

        public PagoController(RepositorioPago repoPago)
        {
            _repo = repoPago;
        }

        //GET: api/pagos/contrato/6
        [HttpGet("contrato/{idContrato}")]
        public async Task<IActionResult> GetPagosPorContrato(int idContrato)
        {
            try
            {
                var pagos = await _repo.GetByContratoAsync(idContrato);
                if (pagos == null || !pagos.Any())
                    return NotFound("No se encontraron pagos para este contrato.");

                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener pagos: {ex.Message}");
            }
        }
    }
}