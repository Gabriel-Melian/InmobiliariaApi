using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InmobiliariaApi.Migrations
{
    /// <inheritdoc />
    public partial class InmuebleFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inmuebles_Propietarios_DuenioIdPropietario",
                table: "Inmuebles");

            migrationBuilder.DropIndex(
                name: "IX_Inmuebles_DuenioIdPropietario",
                table: "Inmuebles");

            migrationBuilder.DropColumn(
                name: "DuenioIdPropietario",
                table: "Inmuebles");

            migrationBuilder.CreateIndex(
                name: "IX_Inmuebles_IdPropietario",
                table: "Inmuebles",
                column: "IdPropietario");

            migrationBuilder.AddForeignKey(
                name: "FK_Inmuebles_Propietarios_IdPropietario",
                table: "Inmuebles",
                column: "IdPropietario",
                principalTable: "Propietarios",
                principalColumn: "IdPropietario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inmuebles_Propietarios_IdPropietario",
                table: "Inmuebles");

            migrationBuilder.DropIndex(
                name: "IX_Inmuebles_IdPropietario",
                table: "Inmuebles");

            migrationBuilder.AddColumn<int>(
                name: "DuenioIdPropietario",
                table: "Inmuebles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inmuebles_DuenioIdPropietario",
                table: "Inmuebles",
                column: "DuenioIdPropietario");

            migrationBuilder.AddForeignKey(
                name: "FK_Inmuebles_Propietarios_DuenioIdPropietario",
                table: "Inmuebles",
                column: "DuenioIdPropietario",
                principalTable: "Propietarios",
                principalColumn: "IdPropietario");
        }
    }
}
