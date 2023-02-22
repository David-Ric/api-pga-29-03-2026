using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class NumeroPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pedido",
                table: "CabecalhoPedidoVenda",
                newName: "pedido");

            migrationBuilder.AlterColumn<string>(
                name: "pedido",
                table: "CabecalhoPedidoVenda",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pedido",
                table: "CabecalhoPedidoVenda",
                newName: "Pedido");

            migrationBuilder.AlterColumn<int>(
                name: "Pedido",
                table: "CabecalhoPedidoVenda",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
