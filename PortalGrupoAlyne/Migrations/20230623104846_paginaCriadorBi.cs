using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class paginaCriadorBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 203, 40, "", 1, "Construtor BI", 4, "/construtor-bi" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 204, 41, "", 5, "Construtor BI", null, "/construtor-bi" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 44, 207, 25, 146, 189, 59, 241, 75, 85, 212, 158, 228, 12, 224, 77, 184, 72, 47, 122, 150, 57, 5, 190, 227, 202, 192, 38, 51, 111, 34, 254, 206, 245, 237, 209, 119, 115, 155, 175, 184, 43, 207, 10, 21, 11, 52, 128, 0, 57, 154, 209, 212, 179, 207, 205, 72, 252, 229, 245, 13, 106, 19, 71, 221 }, new byte[] { 114, 173, 223, 132, 205, 36, 61, 205, 241, 218, 2, 207, 29, 197, 29, 116, 175, 239, 118, 228, 40, 45, 165, 177, 39, 188, 126, 30, 177, 254, 71, 114 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 15, 212, 126, 110, 102, 154, 187, 201, 42, 62, 150, 10, 247, 78, 113, 240, 182, 221, 81, 3, 217, 141, 75, 229, 133, 223, 196, 13, 241, 154, 254, 2, 143, 107, 170, 97, 65, 12, 134, 150, 199, 45, 159, 98, 22, 87, 137, 34, 179, 42, 38, 218, 80, 157, 65, 240, 153, 96, 249, 44, 120, 230, 111, 195 }, new byte[] { 188, 127, 222, 138, 233, 94, 108, 21, 163, 93, 254, 205, 224, 81, 189, 204, 57, 81, 86, 21, 205, 240, 12, 137, 78, 16, 186, 20, 253, 186, 193, 64 } });
        }
    }
}
