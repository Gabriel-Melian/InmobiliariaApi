using System.ComponentModel.DataAnnotations;
namespace InmobiliariaApi.Models
{
    public class Inquilino
    {
        [Key]
        public int IdInquilino { get; set; }

        [Required]
        [StringLength(40)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(40)]
        public string? Apellido { get; set; }

        [Required]
        [StringLength(15)]
        public string? Dni { get; set; }

        [Required]
        [StringLength(255)]
        public string? Direccion { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }
    }
}