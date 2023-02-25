using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class MigracaoStatusPesquisa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Industria");

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Ordem" },
                values: new object[] { 10, 25, "fa fa-cogs", "Configurações", 0 });

            migrationBuilder.InsertData(
                table: "SubMenu",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "Ordem" },
                values: new object[] { 10, 25, "fa fa-cogs", 1, "Configurações", 0 });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 29, 26, "fa fa-refresh", 1, "Restaurar dados sistema", 10, "" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 32, 26, "fa fa-refresh", 10, "Restaurar dados sistema", null, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SubMenu",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Indústria");
        }
    }
}
