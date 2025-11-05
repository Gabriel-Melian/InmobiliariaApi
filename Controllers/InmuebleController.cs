using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaApi.Repositories;
using InmobiliariaApi.Models;
using InmobiliariaApi.Services;
using System.Text.Json;
using InmobiliariaApi.Models.DTOs;

namespace InmobiliariaApi.Controllers
{
    [Authorize]//Accede con el token
    [ApiController]
    [Route("api/[controller]")]
    public class InmuebleController : ControllerBase
    {
        private readonly RepositorioInmueble _repoInmueble;
        private readonly JwtService jwtService;

        public InmuebleController(RepositorioInmueble repositorioInmueble, JwtService jwtService)
        {
            this._repoInmueble = repositorioInmueble;
            this.jwtService = jwtService;
        }

        //GET: api/inmueble
        [HttpGet]
        public async Task<IActionResult> GetInmuebles()
        {
            try
            {
                //Obtener email del token
                var email = User.Identity?.Name;
                if (email == null)
                    return Unauthorized("Token inválido o expirado.");

                var inmuebles = await _repoInmueble.GetByPropietarioEmailAsync(email);

                if (inmuebles == null || inmuebles.Count == 0)
                    return NotFound("No se encontraron inmuebles para este propietario.");

                return Ok(inmuebles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener inmuebles: {ex.Message}");
            }
        }

        //POST: api/inmueble/cargar
        [HttpPost("cargar")]
        public async Task<IActionResult> CargarInmueble([FromForm] IFormFile imagen, [FromForm] string inmueble)
        {
            try
            {
                //Obtener email del token
                var email = User.Identity?.Name;
                if (email == null) return Unauthorized("Token inválido o expirado.");

                //Opciones para aceptar nombres de propiedades case-insensitive
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                //Deserializar con opciones
                var inmuebleData = JsonSerializer.Deserialize<Inmueble>(inmueble, options);

                if (inmuebleData == null)
                    return BadRequest("Datos de inmueble inválidos (JSON vacio o mal formado).");

                //Validaciones basicas: aseguro campos obligatorios
                if (string.IsNullOrWhiteSpace(inmuebleData.Direccion))
                    return BadRequest("Campo 'direccion' es obligatorio.");
                if (string.IsNullOrWhiteSpace(inmuebleData.Uso))
                    return BadRequest("Campo 'uso' es obligatorio.");
                if (string.IsNullOrWhiteSpace(inmuebleData.Tipo))
                    return BadRequest("Campo 'tipo' es obligatorio.");
                //Ambientes, Superficie, Valor, Longitud, Latitud son value types, osea ya tienen valor por defecto 0 si no vienen.
                //Validar > 0:
                if (inmuebleData.Ambientes <= 0)
                    return BadRequest("Campo 'ambientes' debe ser mayor a 0.");
                if (inmuebleData.Superficie <= 0)
                    return BadRequest("Campo 'superficie' debe ser mayor a 0.");
                if (inmuebleData.Valor <= 0)
                    return BadRequest("Campo 'valor' debe ser mayor a 0.");

                //Guardar la imagen si vino
                if (imagen != null && imagen.Length > 0)
                {
                    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    var filePath = Path.Combine(uploadsPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    inmuebleData.Imagen = Path.Combine("Uploads", fileName).Replace("\\", "/");
                }

                //Obtener propietario desde el email del token
                var propietario = await _repoInmueble.GetPropietarioByEmail(email);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");

                //Asignar relacion
                inmuebleData.IdPropietario = propietario.IdPropietario;
                inmuebleData.Duenio = propietario;

                //Si no se especifico, aseguramos valores por defecto
                //(por ejemplo, TieneContratoVigente puede venir false por defecto)
                await _repoInmueble.CreateAsync(inmuebleData);

                return Ok(new { message = "Inmueble cargado correctamente." });
            }
            catch (Exception ex)
            {
                //Mensaje interno para depurar
                var inner = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(ex.ToString());
                return BadRequest($"Error al cargar inmueble: {inner}");
            }
        }

        [HttpPut("actualizar-disponibilidad")]
        public async Task<IActionResult> ActualizarDisponibilidad([FromBody] ActualizarInmueble dto)
        {
            try
            {
                //Obtener email del token
                var email = User.Identity?.Name;
                if (email == null)
                    return Unauthorized("Token inválido o expirado.");

                //Buscar inmueble por ID y propietario (No edita inmuebles de otros propietarios)
                var inmueble = await _repoInmueble.GetByIdAndPropietarioEmailAsync(dto.IdInmueble, email);
                if (inmueble == null)
                    return NotFound("Inmueble no encontrado o no pertenece a este propietario.");

                //Solo disponibilidad
                inmueble.Disponible = dto.Disponible;

                //Guardar cambios
                await _repoInmueble.UpdateAsync(inmueble);

                //Devolver el inmueble actualizado
                return Ok(inmueble);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar disponibilidad: {ex.Message}");
            }
        }

        //GET: api/inmueble/contrato-vigente
        [HttpGet("contrato-vigente")]
        public async Task<IActionResult> GetInmueblesConContratoVigente()
        {
            try
            {
                var email = User.Identity?.Name;
                if (email == null)
                    return Unauthorized("Token inválido o expirado.");

                var inmuebles = await _repoInmueble.GetConContratoVigenteByEmailAsync(email);

                if (inmuebles == null || inmuebles.Count == 0)
                    return NotFound("No se encontraron inmuebles con contrato vigente.");

                return Ok(inmuebles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener inmuebles con contrato vigente: {ex.Message}");
            }
        }
    }
}