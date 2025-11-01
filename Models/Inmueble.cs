using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InmobiliariaApi.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }

        [Required]
        [StringLength(255)]
        public string? Direccion { get; set; }

        [Required]
        [StringLength(20)]
        public string? Uso { get; set; }

        [Required]
        [StringLength(20)]
        public string? Tipo { get; set; }

        [Required]
        public int Ambientes { get; set; }

        [Required]
        public int Superficie { get; set; }

        [Required]
        public double Latitud { get; set; }

        [Required]
        public double Valor { get; set; }

        [StringLength(200)]
        public string? Imagen { get; set; }

        [Required]
        public bool Disponible { get; set; }

        [Required]
        public double Longitud { get; set; }

        [Required]
        [ForeignKey("Duenio")]
        public int IdPropietario { get; set; }

        public Propietario? Duenio { get; set; }

        public bool TieneContratoVigente { get; set; }
    }
}