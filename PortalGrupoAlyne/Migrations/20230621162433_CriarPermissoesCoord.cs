using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class CriarPermissoesCoord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuPermissao",
                columns: new[] { "Id", "Codigo", "GrupoUsuarioId", "Nome", "UsuarioId" },
                values: new object[] { 200, 7, 5, "Consultas", null });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 200, 37, "fa fa-money", 4, "Acompanhamento Vendas", null, "/acompanhamento-vendas" });

            migrationBuilder.InsertData(
                table: "PaginaPermissao",
                columns: new[] { "Id", "Codigo", "GrupoUsuarioId", "MenuPermissaoId", "Nome", "SubMenuPermissaoId", "UsuarioId" },
                values: new object[] { 200, 37, 5, 13, "Acompanhamento Vendas", null, null });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 228, 107, 171, 108, 11, 204, 33, 85, 190, 146, 171, 218, 223, 40, 83, 31, 115, 182, 104, 72, 20, 220, 210, 203, 4, 96, 181, 38, 159, 20, 214, 13, 79, 124, 85, 107, 13, 250, 163, 68, 72, 31, 250, 58, 131, 143, 22, 120, 45, 248, 222, 134, 209, 249, 87, 95, 48, 169, 62, 118, 174, 227, 79 }, new byte[] { 96, 20, 23, 42, 26, 111, 5, 111, 112, 107, 153, 83, 16, 242, 91, 161, 171, 188, 218, 22, 22, 243, 44, 224, 227, 254, 161, 75, 33, 115, 252, 213 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuPermissao",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "PaginaPermissao",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 239, 146, 186, 89, 128, 146, 176, 34, 39, 48, 102, 100, 103, 70, 2, 56, 229, 71, 236, 227, 130, 102, 35, 206, 76, 72, 11, 8, 183, 65, 146, 221, 69, 155, 244, 146, 169, 154, 162, 115, 26, 225, 50, 4, 227, 27, 164, 217, 38, 251, 94, 86, 236, 122, 210, 32, 171, 170, 188, 184, 125, 207, 126 }, new byte[] { 141, 245, 104, 24, 147, 111, 129, 191, 38, 151, 228, 205, 71, 201, 118, 31, 101, 14, 238, 151, 88, 208, 176, 28, 105, 89, 48, 121, 251, 28, 64, 1 } });
        }
    }
}
