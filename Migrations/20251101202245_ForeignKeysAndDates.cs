using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InmobiliariaApi.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeysAndDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Inmuebles_InmuebleIdInmueble",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Inquilinos_InquilinoIdInquilino",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Contratos_ContratoIdContrato",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_ContratoIdContrato",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_InmuebleIdInmueble",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_InquilinoIdInquilino",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "ContratoIdContrato",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "InmuebleIdInmueble",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "InquilinoIdInquilino",
                table: "Contratos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Contratos",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Contratos",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdContrato",
                table: "Pagos",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_IdInmueble",
                table: "Contratos",
                column: "IdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_IdInquilino",
                table: "Contratos",
                column: "IdInquilino");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Inmuebles_IdInmueble",
                table: "Contratos",
                column: "IdInmueble",
                principalTable: "Inmuebles",
                principalColumn: "IdInmueble",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Inquilinos_IdInquilino",
                table: "Contratos",
                column: "IdInquilino",
                principalTable: "Inquilinos",
                principalColumn: "IdInquilino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Contratos_IdContrato",
                table: "Pagos",
                column: "IdContrato",
                principalTable: "Contratos",
                principalColumn: "IdContrato",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Inmuebles_IdInmueble",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Inquilinos_IdInquilino",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Contratos_IdContrato",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdContrato",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_IdInmueble",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_IdInquilino",
                table: "Contratos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "ContratoIdContrato",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Contratos",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Contratos",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "InmuebleIdInmueble",
                table: "Contratos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InquilinoIdInquilino",
                table: "Contratos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_ContratoIdContrato",
                table: "Pagos",
                column: "ContratoIdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_InmuebleIdInmueble",
                table: "Contratos",
                column: "InmuebleIdInmueble");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_InquilinoIdInquilino",
                table: "Contratos",
                column: "InquilinoIdInquilino");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Inmuebles_InmuebleIdInmueble",
                table: "Contratos",
                column: "InmuebleIdInmueble",
                principalTable: "Inmuebles",
                principalColumn: "IdInmueble");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Inquilinos_InquilinoIdInquilino",
                table: "Contratos",
                column: "InquilinoIdInquilino",
                principalTable: "Inquilinos",
                principalColumn: "IdInquilino");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Contratos_ContratoIdContrato",
                table: "Pagos",
                column: "ContratoIdContrato",
                principalTable: "Contratos",
                principalColumn: "IdContrato");
        }
    }
}
