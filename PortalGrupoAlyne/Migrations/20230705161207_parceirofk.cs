using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class parceirofk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CabecalhoPedidoVenda_Parceiro_ParceiroId",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.DropIndex(
                name: "IX_CabecalhoPedidoVenda_ParceiroId",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 81, 208, 34, 125, 186, 251, 203, 19, 249, 33, 238, 133, 4, 159, 213, 215, 251, 209, 37, 194, 195, 247, 38, 209, 252, 236, 120, 0, 108, 159, 41, 103, 34, 243, 8, 186, 200, 169, 74, 213, 71, 26, 137, 116, 111, 33, 190, 220, 174, 245, 240, 180, 35, 112, 110, 195, 51, 33, 98, 0, 87, 76, 120, 145 }, new byte[] { 110, 238, 106, 126, 207, 238, 196, 93, 111, 26, 2, 141, 70, 220, 77, 131, 183, 64, 59, 243, 135, 80, 77, 238, 85, 58, 133, 185, 208, 86, 87, 10 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 4, 195, 138, 200, 182, 75, 121, 78, 5, 180, 223, 96, 179, 91, 83, 213, 249, 71, 113, 14, 94, 201, 181, 13, 53, 42, 166, 206, 94, 135, 163, 223, 127, 102, 173, 94, 66, 51, 46, 102, 137, 26, 4, 234, 8, 192, 139, 219, 221, 76, 154, 115, 28, 49, 241, 161, 10, 254, 51, 14, 176, 166, 26, 219 }, new byte[] { 254, 205, 241, 69, 108, 139, 163, 107, 112, 127, 48, 169, 34, 11, 96, 163, 180, 219, 3, 137, 235, 150, 242, 4, 138, 197, 83, 193, 105, 169, 28, 169 } });

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
    }
}
