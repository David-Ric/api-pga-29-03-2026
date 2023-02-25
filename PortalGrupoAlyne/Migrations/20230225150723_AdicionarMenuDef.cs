using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class AdicionarMenuDef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Ordem" },
                values: new object[,]
                {
                    { 2, 4, "fa fa-address-card", "Cadastros", 0 },
                    { 3, 5, "fa fa-map-o", "Movimentos", 0 },
                    { 4, 7, "fa fa-search-minus", "Consultas", 0 },
                    { 5, 6, "fa fa-object-ungroup", "Outros", 0 }
                });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 17, 13, "fa fa-briefcase", 2, "Empresas", null, "/cadastro-tipo-empresa" },
                    { 18, 13, "fa fa-user-plus", 2, "Vendedores", null, "/cadastro-vendedores" },
                    { 19, 18, "fa fa-credit-card", 2, "Tipo de Negociação", null, "/cadastro-tipo-negociacao" },
                    { 20, 14, "fa fa-users", 2, "Parceiros", null, "/cadastro-parceiros" },
                    { 21, 12, "fa fa-shopping-bag", 2, "Grupo de Produtos", null, "/cadastro-grupos-produtos" },
                    { 22, 11, "fa fa-cart-plus", 2, "Produtos", null, "/cadastro-produtos" },
                    { 23, 16, "fa fa-user-times", 2, "Concorrentes", null, "/cadastro-concorrentes" },
                    { 24, 17, "fa fa-user-times", 2, "Produto x Concorrente", null, "/produtos-concorrentes" },
                    { 25, 19, "fa fa-calculator", 2, "Tabela de Preço", null, "/tabela-de-preco" },
                    { 26, 23, "fa fa-line-chart", 3, "Pedido de Vendas", null, "/pedido_vendas" },
                    { 27, 9, "fa fa-user-circle-o", 5, "Usuarios", null, "/cadastro-usuarios" },
                    { 28, 10, "fa fa-users", 5, "Grupo de Usuarios", null, "/cadastro-grupo-usuarios" },
                    { 31, 24, "fa fa-external-link-square", 5, "Receber dados Sankhya", null, "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 14, 22, "fa fa-newspaper-o", 1, "Montar Menu", 4, "/montar-menu" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 15, 21, "fa fa-id-card-o", 1, "Cadastro de Páginas", 4, "/cadastro-de-paginas" });
        }
    }
}
