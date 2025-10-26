using System.ComponentModel.DataAnnotations;
namespace InmobiliariaApi.Models
{
    public class Contrato
    {
        [Key]
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public double MontoAlquiler { get; set; }
        public bool Estado { get; set; }
        public int IdInquilino { get; set; }
        public int IdInmueble { get; set; }
        public Inquilino? Inquilino { get; set; }
        public Inmueble? Inmueble { get; set; }
    }
}

/*
{
"idContrato": 14,
"fechaInicio": "2016-03-02",
"fechaFinalizacion": "2020-07-03",
"montoAlquiler": 324423.00,
"estado": true,
"idInquilino": 6,
"idInmueble": 1,
"inquilino": {
    "idInquilino": 6,
    "nombre": "Lautaro",
    "apellido": "Martinez",
    "dni": "999",
    "telefono": "8884453",
    "email": "toro@gmail.com"
},
"inmueble": {
    "idInmueble": 1,
    "direccion": "Belgrano 123",
    "uso": "Residencial",
    "tipo": "Departamento",
    "ambientes": 2,
    "superficie": 131,
    "latitud": 6.0,
    "valor": 34324.0,
    "imagen": "Uploads\\\\avatar_4.jpg",
    "disponible": false,
    "longitud": 1.0,
    "idPropietario": 3,
    "duenio": {
        "idPropietario": 3,
        "nombre": "Luis",
        "apellido": "Mercado",
        "dni": "25102025",
        "telefono": "2664466511",
        "email": "luisprofessor@gmail.com",
        "clave": "SQZNIpUXTSDNE2bcQ9j0TkNKhh88O20djkB0De8URow="
    },
    "tieneContratoVigente": false
}
}
*/