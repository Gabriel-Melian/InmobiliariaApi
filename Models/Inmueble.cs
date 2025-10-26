using System.ComponentModel.DataAnnotations;
namespace InmobiliariaApi.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }
        public string? Direccion { get; set; }
        public string? Uso { get; set; }
        public string? Tipo { get; set; }
        public int Ambientes { get; set; }
        public int Superficie { get; set; }
        public double Latitud { get; set; }
        public double Valor { get; set; }
        public string? Imagen { get; set; }
        public bool Disponible { get; set; }
        public double Longitud { get; set; }
        public int IdPropietario { get; set; }
        public Propietario? Duenio { get; set; }
        public bool TieneContratoVigente { get; set; }
    }
}