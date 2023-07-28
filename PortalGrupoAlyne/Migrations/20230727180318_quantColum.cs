using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class quantColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Colunas",
                table: "Etiqueta",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 78, 88, 92, 171, 154, 17, 52, 26, 251, 45, 207, 131, 71, 11, 237, 27, 66, 229, 217, 241, 136, 141, 54, 111, 61, 87, 213, 236, 45, 155, 128, 41, 29, 192, 58, 114, 123, 34, 186, 233, 246, 136, 213, 86, 69, 153, 236, 40, 68, 76, 53, 41, 32, 90, 179, 17, 144, 198, 48, 139, 14, 111, 159, 243 }, new byte[] { 66, 175, 170, 147, 226, 134, 12, 217, 44, 86, 67, 98, 121, 175, 22, 64, 27, 2, 119, 64, 137, 17, 204, 227, 121, 1, 124, 116, 139, 88, 121, 4 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colunas",
                table: "Etiqueta");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 41, 249, 174, 169, 91, 95, 99, 106, 60, 22, 178, 150, 216, 241, 218, 88, 123, 208, 32, 86, 128, 186, 246, 189, 133, 28, 46, 144, 46, 45, 29, 33, 53, 32, 167, 160, 97, 167, 97, 193, 183, 175, 54, 215, 117, 236, 208, 141, 63, 200, 33, 9, 75, 78, 15, 135, 234, 139, 239, 136, 59, 99, 239, 145 }, new byte[] { 243, 35, 209, 150, 196, 40, 203, 40, 78, 157, 43, 225, 190, 240, 105, 220, 14, 143, 229, 92, 147, 42, 234, 184, 248, 164, 215, 162, 44, 42, 73, 157 } });
        }
    }
}
