using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class NumeroPedidoForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidoVenda_CabecalhoPedidoVenda_CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda");

            migrationBuilder.AlterColumn<int>(
                name: "CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidoVenda_CabecalhoPedidoVenda_CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda",
                column: "CabecalhoPedidoVendaId",
                principalTable: "CabecalhoPedidoVenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidoVenda_CabecalhoPedidoVenda_CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda");

            migrationBuilder.AlterColumn<int>(
                name: "CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidoVenda_CabecalhoPedidoVenda_CabecalhoPedidoVendaId",
                table: "ItemPedidoVenda",
                column: "CabecalhoPedidoVendaId",
                principalTable: "CabecalhoPedidoVenda",
                principalColumn: "Id");
        }
    }
}
