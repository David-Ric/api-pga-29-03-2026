using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class horasessao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HoraAcesso",
                table: "Sessao",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 230, 14, 184, 97, 98, 212, 176, 197, 41, 80, 45, 17, 112, 65, 224, 151, 36, 230, 85, 3, 101, 125, 21, 254, 121, 59, 180, 138, 93, 214, 226, 54, 10, 115, 123, 193, 34, 205, 82, 121, 6, 53, 235, 85, 237, 158, 249, 52, 236, 92, 97, 235, 170, 80, 98, 180, 215, 56, 176, 19, 5, 63, 42, 126 }, new byte[] { 38, 237, 38, 225, 41, 241, 29, 1, 187, 121, 253, 135, 253, 11, 150, 205, 173, 19, 58, 156, 96, 179, 22, 39, 231, 139, 14, 59, 141, 218, 21, 37 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraAcesso",
                table: "Sessao");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 19, 195, 106, 243, 108, 113, 56, 105, 174, 255, 200, 204, 189, 28, 194, 153, 143, 87, 182, 199, 20, 81, 86, 12, 90, 138, 54, 180, 84, 52, 1, 96, 199, 24, 171, 1, 200, 139, 55, 107, 177, 85, 136, 250, 139, 138, 27, 214, 190, 138, 96, 129, 7, 216, 126, 92, 215, 34, 62, 102, 116, 26, 241, 179 }, new byte[] { 108, 38, 187, 34, 49, 230, 235, 85, 120, 58, 198, 145, 125, 253, 227, 3, 132, 215, 167, 238, 149, 208, 65, 155, 184, 95, 127, 185, 166, 51, 168, 119 } });
        }
    }
}
