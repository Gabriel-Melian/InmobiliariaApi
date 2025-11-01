using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaApi.Repositories;
using InmobiliariaApi.Models;
using InmobiliariaApi.Services;

namespace InmobiliariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]//Accede unicamente con el Token
    public class PropietarioController : ControllerBase
    {
        private readonly RepositorioPropietario _repo;
        private readonly JwtService _jwtService;

        public PropietarioController(RepositorioPropietario repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
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

        //Editar perfil del propietario autenticado
        [HttpPut("editar")]
        public async Task<IActionResult> EditarPerfil([FromBody] Propietario datos)
        {
            try
            {
                //Obtener email actual del token
                var emailToken = User.Identity?.Name;
                if (emailToken == null)
                    return Unauthorized("Token inválido o expirado.");

                var propietario = await _repo.GetByEmailAsync(emailToken);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");

                //Actualizar campos
                propietario.Nombre = datos.Nombre ?? propietario.Nombre;
                propietario.Apellido = datos.Apellido ?? propietario.Apellido;
                propietario.Dni = datos.Dni ?? propietario.Dni;
                propietario.Telefono = datos.Telefono ?? propietario.Telefono;

                //*Para ver si modifico el email*
                bool emailCambiado = false;

                if (!string.IsNullOrWhiteSpace(datos.Email) && datos.Email != propietario.Email)
                {
                    propietario.Email = datos.Email;
                    emailCambiado = true;
                }

                await _repo.UpdateAsync(propietario);

                //Si cambio el email, genera un token nuevo
                if (emailCambiado)
                {
                    var nuevoToken = _jwtService.GenerateToken(propietario);
                    return Ok(new
                    {
                        message = "Perfil actualizado correctamente. Nuevo token generado.",
                        token = nuevoToken
                    });
                }

                return Ok(new { message = "Perfil actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al editar perfil: {ex.Message}");
            }
        }

        //Cambiar clave del propietario autenticado
        [HttpPut("cambiar-clave")]
        public async Task<IActionResult> CambiarClave([FromForm] string currentPassword, [FromForm] string newPassword)
        {
            try
            {
                //Obtiene el email del token
                var email = User.Identity?.Name;
                if (email == null)
                    return Unauthorized("Token inválido o expirado.");

                //Busca el propietario
                var propietario = await _repo.GetByEmailAsync(email);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");

                //Verifica y compara clave actual con la que ingresa el propietario
                if (!BCrypt.Net.BCrypt.Verify(currentPassword, propietario.Clave))
                    return Unauthorized("La clave actual no es correcta.");

                //Hashea la nueva clave
                propietario.Clave = BCrypt.Net.BCrypt.HashPassword(newPassword);

                //Actualiza en base de datos
                await _repo.UpdateAsync(propietario);

                return Ok("Clave actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al cambiar clave: {ex.Message}");
            }
        }

        [HttpPut("restablecer")]
        [AllowAnonymous]//Esto es solo para TESTEO!!!
        public async Task<IActionResult> Restablecer()
        {
            try
            {
                //Buscar el propietario que se debe restablecer
                var propietario = await _repo.GetByIdAsync(1);
                if (propietario == null)
                {
                    //Si no existe, lo creamos desde cero por si se borro
                    propietario = new Propietario
                    {
                        Nombre = "Gabriel",
                        Apellido = "Melian",
                        Dni = "44556677",
                        Telefono = "2664785566",
                        Email = "gab24@gmail.com",
                        Clave = BCrypt.Net.BCrypt.HashPassword("DEEKQW")
                    };

                    await _repo.CreateAsync(propietario);
                }
                else
                {
                    //Si existe, lo restablecemos a los valores por defecto
                    propietario.Email = "gab24@gmail.com";
                    propietario.Clave = BCrypt.Net.BCrypt.HashPassword("DEEKQW");
                    await _repo.UpdateAsync(propietario);
                }

                return Ok("Propietario restablecido correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al restablecer propietario: {ex.Message}");
            }
        }
    }
}