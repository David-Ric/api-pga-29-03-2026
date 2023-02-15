using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class alteracaotabusuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GrupoId",
                table: "Usuario",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_GrupoUsuario_GrupoId",
                table: "Usuario",
                column: "GrupoId",
                principalTable: "GrupoUsuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_GrupoUsuario_GrupoId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_GrupoId",
                table: "Usuario");
        }
    }
}
