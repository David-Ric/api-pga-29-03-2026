using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class temposessao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempoSessao",
                table: "Configuracao",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 210, 102, 125, 46, 18, 20, 234, 200, 35, 190, 92, 63, 224, 37, 246, 114, 156, 129, 36, 137, 72, 65, 231, 161, 216, 146, 209, 159, 244, 36, 209, 69, 97, 164, 37, 100, 192, 173, 189, 131, 160, 140, 38, 36, 78, 127, 80, 220, 192, 214, 97, 129, 105, 108, 120, 248, 190, 16, 139, 98, 60, 47, 147 }, new byte[] { 38, 168, 24, 144, 66, 0, 205, 29, 27, 38, 86, 109, 39, 204, 142, 225, 5, 64, 65, 45, 222, 178, 183, 101, 134, 196, 35, 125, 128, 33, 189, 239 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoSessao",
                table: "Configuracao");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 230, 14, 184, 97, 98, 212, 176, 197, 41, 80, 45, 17, 112, 65, 224, 151, 36, 230, 85, 3, 101, 125, 21, 254, 121, 59, 180, 138, 93, 214, 226, 54, 10, 115, 123, 193, 34, 205, 82, 121, 6, 53, 235, 85, 237, 158, 249, 52, 236, 92, 97, 235, 170, 80, 98, 180, 215, 56, 176, 19, 5, 63, 42, 126 }, new byte[] { 38, 237, 38, 225, 41, 241, 29, 1, 187, 121, 253, 135, 253, 11, 150, 205, 173, 19, 58, 156, 96, 179, 22, 39, 231, 139, 14, 59, 141, 218, 21, 37 } });
        }
    }
}
