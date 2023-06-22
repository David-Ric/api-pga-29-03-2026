using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class PaginasSessaoLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 201, 38, "", 1, "Sessões em uso", 4, "/sessoes-em-uso" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 202, 39, "", 1, "Log Ações", 4, "/log-acoes" });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 15, 212, 126, 110, 102, 154, 187, 201, 42, 62, 150, 10, 247, 78, 113, 240, 182, 221, 81, 3, 217, 141, 75, 229, 133, 223, 196, 13, 241, 154, 254, 2, 143, 107, 170, 97, 65, 12, 134, 150, 199, 45, 159, 98, 22, 87, 137, 34, 179, 42, 38, 218, 80, 157, 65, 240, 153, 96, 249, 44, 120, 230, 111, 195 }, new byte[] { 188, 127, 222, 138, 233, 94, 108, 21, 163, 93, 254, 205, 224, 81, 189, 204, 57, 81, 86, 21, 205, 240, 12, 137, 78, 16, 186, 20, 253, 186, 193, 64 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 228, 107, 171, 108, 11, 204, 33, 85, 190, 146, 171, 218, 223, 40, 83, 31, 115, 182, 104, 72, 20, 220, 210, 203, 4, 96, 181, 38, 159, 20, 214, 13, 79, 124, 85, 107, 13, 250, 163, 68, 72, 31, 250, 58, 131, 143, 22, 120, 45, 248, 222, 134, 209, 249, 87, 95, 48, 169, 62, 118, 174, 227, 79 }, new byte[] { 96, 20, 23, 42, 26, 111, 5, 111, 112, 107, 153, 83, 16, 242, 91, 161, 171, 188, 218, 22, 22, 243, 44, 224, 227, 254, 161, 75, 33, 115, 252, 213 } });
        }
    }
}
