using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;//Evitar mostrar la clave hasheada en el JSON
namespace InmobiliariaApi.Models
{
    public class Propietario
    {
        [Key]
        public int IdPropietario { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Dni { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        [StringLength(255)]
        public string Clave { get; set; } = string.Empty;

    }
}