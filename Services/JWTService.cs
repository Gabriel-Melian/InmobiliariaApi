using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using InmobiliariaApi.Models;

namespace InmobiliariaApi.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Propietario propietario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, propietario.IdPropietario.ToString()),
                new Claim(ClaimTypes.Name, propietario.Email ?? ""),
                new Claim("NombreCompleto", $"{propietario.Nombre} {propietario.Apellido}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}