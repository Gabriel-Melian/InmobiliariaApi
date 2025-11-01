using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InmobiliariaApi.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaPago { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Monto { get; set; }

        [MaxLength(100)]
        public string? Detalle { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public int IdContrato { get; set; }

        [ForeignKey(nameof(IdContrato))]
        public Contrato? Contrato { get; set; }
    }
}

/*
{
        "idPago": 1,
        "fechaPago": "2024-04-10",
        "monto": 23.00,
        "detalle": "Mes abril",
        "estado": false,
        "idContrato": 6,
        "contrato": {
            "idContrato": 6,
            "fechaInicio": "2025-08-01",
            "fechaFinalizacion": "2028-08-01",
            "montoAlquiler": 23.00,
            "estado": true,
            "idInquilino": 1,
            "idInmueble": 13,
            "inquilino": null,
            "inmueble": null
        }
    }
*/