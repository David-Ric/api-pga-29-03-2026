using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class migracaocodparceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParceiroId",
                table: "CabecalhoPedidoVenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CabecalhoPedidoVenda_ParceiroId",
                table: "CabecalhoPedidoVenda",
                column: "ParceiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
                table: "CabecalhoPedidoVenda",
                column: "ParceiroId",
                principalTable: "Parceiro",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.DropIndex(
                name: "IX_CabecalhoPedidoVenda_ParceiroId",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.DropColumn(
                name: "ParceiroId",
                table: "CabecalhoPedidoVenda");
        }
    }
}
