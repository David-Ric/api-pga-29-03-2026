using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class TempoSessaoUsuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempoSessao",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempoSessao",
                table: "GrupoUsuario",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 251, 136, 66, 189, 30, 196, 248, 94, 149, 132, 49, 102, 150, 107, 30, 31, 101, 188, 116, 40, 159, 188, 158, 118, 254, 123, 166, 142, 9, 152, 87, 74, 41, 214, 184, 44, 92, 46, 12, 42, 245, 243, 174, 248, 231, 71, 213, 19, 225, 227, 187, 228, 165, 39, 233, 156, 90, 50, 185, 233, 204, 185, 34 }, new byte[] { 75, 169, 12, 68, 87, 62, 42, 109, 23, 203, 61, 49, 254, 37, 50, 31, 248, 59, 126, 8, 147, 128, 218, 15, 61, 0, 130, 167, 57, 91, 102, 255 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoSessao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TempoSessao",
                table: "GrupoUsuario");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 210, 102, 125, 46, 18, 20, 234, 200, 35, 190, 92, 63, 224, 37, 246, 114, 156, 129, 36, 137, 72, 65, 231, 161, 216, 146, 209, 159, 244, 36, 209, 69, 97, 164, 37, 100, 192, 173, 189, 131, 160, 140, 38, 36, 78, 127, 80, 220, 192, 214, 97, 129, 105, 108, 120, 248, 190, 16, 139, 98, 60, 47, 147 }, new byte[] { 38, 168, 24, 144, 66, 0, 205, 29, 27, 38, 86, 109, 39, 204, 142, 225, 5, 64, 65, 45, 222, 178, 183, 101, 134, 196, 35, 125, 128, 33, 189, 239 } });
        }
    }
}
