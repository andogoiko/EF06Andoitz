using Microsoft.EntityFrameworkCore.Migrations;

namespace EFExamen.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_ModuloId",
                table: "Matriculas",
                column: "ModuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Alumnos_AlumnoId",
                table: "Matriculas",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "AlumnoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Modulos_ModuloId",
                table: "Matriculas",
                column: "ModuloId",
                principalTable: "Modulos",
                principalColumn: "ModuloId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Alumnos_AlumnoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Modulos_ModuloId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_ModuloId",
                table: "Matriculas");
        }
    }
}
