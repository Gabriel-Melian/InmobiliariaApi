using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;//Evitar mostrar la clave hasheada en el JSON
namespace InmobiliariaApi.Models
{
    public class Propietario
    {
        [Key]
        public int IdPropietario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        
        [JsonIgnore]
        public string? Clave { get; set; }

    }
}