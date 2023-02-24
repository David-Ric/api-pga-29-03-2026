using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class novaMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendedor",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parceiro",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<int>(
            //    name: "ParceiroId",
            //    table: "CabecalhoPedidoVenda",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[] { 16, 24, "fa fa-external-link-square", 1, "Receber dados Sankhya", 4, "" });

            migrationBuilder.InsertData(
                table: "PaginaBase",
                columns: new[] { "Id", "Codigo", "Icon", "Nome", "Url" },
                values: new object[] { 24, 24, "fa fa-external-link-square", "Receber dados Sankhya", "" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_CabecalhoPedidoVenda_ParceiroId",
            //    table: "CabecalhoPedidoVenda",
            //    column: "ParceiroId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
            //    table: "CabecalhoPedidoVenda",
            //    column: "ParceiroId",
            //    principalTable: "Parceiro",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
            //    table: "CabecalhoPedidoVenda");

            //migrationBuilder.DropIndex(
            //    name: "IX_CabecalhoPedidoVenda_ParceiroId",
            //    table: "CabecalhoPedidoVenda");

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PaginaBase",
                keyColumn: "Id",
                keyValue: 24);

            //migrationBuilder.DropColumn(
            //    name: "ParceiroId",
            //    table: "CabecalhoPedidoVenda");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendedor",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parceiro",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
