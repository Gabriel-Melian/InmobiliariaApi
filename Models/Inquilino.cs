using System.ComponentModel.DataAnnotations;
namespace InmobiliariaApi.Models
{
    public class Inquilino
    {
        [Key]
        public int IdInquilino { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Dni { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}