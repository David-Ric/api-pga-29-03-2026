using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class correcaoPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComunicadoId",
                table: "PostLido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComunicadoId",
                table: "ComunicadoLido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 122, 201, 206, 232, 216, 116, 112, 175, 27, 35, 134, 229, 41, 150, 219, 172, 234, 108, 237, 168, 25, 88, 245, 229, 237, 159, 76, 254, 86, 87, 72, 22, 109, 197, 61, 53, 86, 180, 78, 186, 164, 226, 60, 192, 249, 144, 19, 74, 231, 144, 197, 45, 125, 171, 74, 167, 151, 252, 148, 135, 84, 180, 25, 237 }, new byte[] { 152, 7, 188, 202, 89, 91, 3, 241, 80, 79, 11, 116, 245, 155, 195, 152, 164, 217, 62, 171, 163, 155, 79, 235, 36, 50, 248, 224, 197, 245, 43, 119 } });

            migrationBuilder.CreateIndex(
                name: "IX_ComunicadoLido_UsuarioID",
                table: "ComunicadoLido",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_ComunicadoLido_Usuario_UsuarioID",
                table: "ComunicadoLido",
                column: "UsuarioID",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComunicadoLido_Usuario_UsuarioID",
                table: "ComunicadoLido");

            migrationBuilder.DropIndex(
                name: "IX_ComunicadoLido_UsuarioID",
                table: "ComunicadoLido");

            migrationBuilder.DropColumn(
                name: "ComunicadoId",
                table: "PostLido");

            migrationBuilder.DropColumn(
                name: "ComunicadoId",
                table: "ComunicadoLido");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 205, 15, 33, 202, 95, 222, 203, 44, 28, 241, 144, 21, 33, 4, 236, 250, 65, 45, 231, 105, 68, 76, 53, 110, 232, 30, 45, 83, 242, 219, 86, 221, 11, 97, 111, 251, 28, 249, 241, 215, 57, 244, 204, 86, 149, 143, 186, 230, 241, 114, 140, 9, 37, 1, 55, 225, 215, 92, 104, 210, 167, 130, 33, 14 }, new byte[] { 149, 180, 66, 79, 99, 123, 63, 209, 61, 216, 143, 24, 172, 0, 169, 4, 230, 107, 203, 205, 164, 219, 226, 217, 139, 18, 106, 248, 5, 219, 4, 174 } });
        }
    }
}
