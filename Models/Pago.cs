using System.ComponentModel.DataAnnotations;
namespace InmobiliariaApi.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string? Detalle { get; set; }
        public bool Estado { get; set; }
        public int IdContrato { get; set; }
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