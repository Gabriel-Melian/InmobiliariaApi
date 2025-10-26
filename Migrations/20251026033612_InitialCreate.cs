using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InmobiliariaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inquilinos",
                columns: table => new
                {
                    IdInquilino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dni = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquilinos", x => x.IdInquilino);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Propietarios",
                columns: table => new
                {
                    IdPropietario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dni = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propietarios", x => x.IdPropietario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inmuebles",
                columns: table => new
                {
                    IdInmueble = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Direccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Uso = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ambientes = table.Column<int>(type: "int", nullable: false),
                    Superficie = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<double>(type: "double", nullable: false),
                    Valor = table.Column<double>(type: "double", nullable: false),
                    Imagen = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Disponible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Longitud = table.Column<double>(type: "double", nullable: false),
                    IdPropietario = table.Column<int>(type: "int", nullable: false),
                    DuenioIdPropietario = table.Column<int>(type: "int", nullable: true),
                    TieneContratoVigente = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inmuebles", x => x.IdInmueble);
                    table.ForeignKey(
                        name: "FK_Inmuebles_Propietarios_DuenioIdPropietario",
                        column: x => x.DuenioIdPropietario,
                        principalTable: "Propietarios",
                        principalColumn: "IdPropietario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    IdContrato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MontoAlquiler = table.Column<double>(type: "double", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdInquilino = table.Column<int>(type: "int", nullable: false),
                    IdInmueble = table.Column<int>(type: "int", nullable: false),
                    InquilinoIdInquilino = table.Column<int>(type: "int", nullable: true),
                    InmuebleIdInmueble = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.IdContrato);
                    table.ForeignKey(
                        name: "FK_Contratos_Inmuebles_InmuebleIdInmueble",
                        column: x => x.InmuebleIdInmueble,
                        principalTable: "Inmuebles",
                        principalColumn: "IdInmueble");
                    table.ForeignKey(
                        name: "FK_Contratos_Inquilinos_InquilinoIdInquilino",
                        column: x => x.InquilinoIdInquilino,
                        principalTable: "Inquilinos",
                        principalColumn: "IdInquilino");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaPago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Detalle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    ContratoIdContrato = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pagos_Contratos_ContratoIdContrato",
                        column: x => x.ContratoIdContrato,
                        principalTable: "Contratos",
                        principalColumn: "IdContrato");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_InmuebleIdInmueble",
                table: "Contratos",
                column: "InmuebleIdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_InquilinoIdInquilino",
                table: "Contratos",
                column: "InquilinoIdInquilino");

            migrationBuilder.CreateIndex(
                name: "IX_Inmuebles_DuenioIdPropietario",
                table: "Inmuebles",
                column: "DuenioIdPropietario");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_ContratoIdContrato",
                table: "Pagos",
                column: "ContratoIdContrato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Inmuebles");

            migrationBuilder.DropTable(
                name: "Inquilinos");

            migrationBuilder.DropTable(
                name: "Propietarios");
        }
    }
}
