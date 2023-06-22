using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class CriarGrupoCoord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GrupoUsuario",
                columns: new[] { "Id", "Nome", "TempoSessao" },
                values: new object[] { 5, "Coordenador", null });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 239, 146, 186, 89, 128, 146, 176, 34, 39, 48, 102, 100, 103, 70, 2, 56, 229, 71, 236, 227, 130, 102, 35, 206, 76, 72, 11, 8, 183, 65, 146, 221, 69, 155, 244, 146, 169, 154, 162, 115, 26, 225, 50, 4, 227, 27, 164, 217, 38, 251, 94, 86, 236, 122, 210, 32, 171, 170, 188, 184, 125, 207, 126 }, new byte[] { 141, 245, 104, 24, 147, 111, 129, 191, 38, 151, 228, 205, 71, 201, 118, 31, 101, 14, 238, 151, 88, 208, 176, 28, 105, 89, 48, 121, 251, 28, 64, 1 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GrupoUsuario",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 251, 136, 66, 189, 30, 196, 248, 94, 149, 132, 49, 102, 150, 107, 30, 31, 101, 188, 116, 40, 159, 188, 158, 118, 254, 123, 166, 142, 9, 152, 87, 74, 41, 214, 184, 44, 92, 46, 12, 42, 245, 243, 174, 248, 231, 71, 213, 19, 225, 227, 187, 228, 165, 39, 233, 156, 90, 50, 185, 233, 204, 185, 34 }, new byte[] { 75, 169, 12, 68, 87, 62, 42, 109, 23, 203, 61, 49, 254, 37, 50, 31, 248, 59, 126, 8, 147, 128, 218, 15, 61, 0, 130, 167, 57, 91, 102, 255 } });
        }
    }
}
