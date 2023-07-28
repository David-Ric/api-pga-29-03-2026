using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class configImpress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "printerAddress",
                table: "Etiqueta",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 200, 153, 204, 142, 111, 218, 166, 120, 234, 27, 253, 195, 79, 147, 249, 215, 255, 189, 27, 33, 38, 1, 140, 46, 245, 44, 229, 215, 208, 202, 144, 208, 241, 141, 109, 255, 117, 207, 150, 82, 82, 107, 117, 138, 81, 124, 124, 219, 224, 0, 161, 205, 2, 244, 75, 248, 61, 91, 93, 164, 12, 165, 141, 208 }, new byte[] { 149, 113, 161, 89, 31, 106, 215, 133, 178, 44, 57, 85, 4, 75, 199, 142, 172, 240, 228, 18, 217, 99, 88, 106, 70, 217, 93, 239, 9, 21, 176, 245 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "printerAddress",
                table: "Etiqueta");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 78, 88, 92, 171, 154, 17, 52, 26, 251, 45, 207, 131, 71, 11, 237, 27, 66, 229, 217, 241, 136, 141, 54, 111, 61, 87, 213, 236, 45, 155, 128, 41, 29, 192, 58, 114, 123, 34, 186, 233, 246, 136, 213, 86, 69, 153, 236, 40, 68, 76, 53, 41, 32, 90, 179, 17, 144, 198, 48, 139, 14, 111, 159, 243 }, new byte[] { 66, 175, 170, 147, 226, 134, 12, 217, 44, 86, 67, 98, 121, 175, 22, 64, 27, 2, 119, 64, 137, 17, 204, 227, 121, 1, 124, 116, 139, 88, 121, 4 } });
        }
    }
}
