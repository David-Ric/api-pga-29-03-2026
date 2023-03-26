using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class criarDataComunicado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "ComunicadoComercial",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 205, 15, 33, 202, 95, 222, 203, 44, 28, 241, 144, 21, 33, 4, 236, 250, 65, 45, 231, 105, 68, 76, 53, 110, 232, 30, 45, 83, 242, 219, 86, 221, 11, 97, 111, 251, 28, 249, 241, 215, 57, 244, 204, 86, 149, 143, 186, 230, 241, 114, 140, 9, 37, 1, 55, 225, 215, 92, 104, 210, 167, 130, 33, 14 }, new byte[] { 149, 180, 66, 79, 99, 123, 63, 209, 61, 216, 143, 24, 172, 0, 169, 4, 230, 107, 203, 205, 164, 219, 226, 217, 139, 18, 106, 248, 5, 219, 4, 174 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "ComunicadoComercial");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 182, 245, 153, 41, 252, 215, 140, 112, 158, 132, 250, 133, 29, 253, 107, 94, 210, 10, 145, 205, 75, 96, 19, 184, 69, 31, 98, 214, 139, 120, 155, 0, 84, 90, 87, 105, 153, 165, 232, 247, 107, 80, 211, 160, 124, 220, 220, 49, 94, 124, 155, 170, 65, 45, 195, 209, 77, 107, 57, 3, 0, 192, 20, 31 }, new byte[] { 255, 137, 71, 136, 142, 216, 112, 147, 92, 148, 94, 254, 125, 144, 0, 95, 129, 98, 61, 79, 246, 226, 235, 131, 218, 110, 199, 90, 193, 47, 37, 37 } });
        }
    }
}
