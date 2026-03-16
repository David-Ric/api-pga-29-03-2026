using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
/**===============================================================

Titulo:VersaoNoPedido.CS
Data: 03/10/2023
Versão: 1.1.017
Autor: David Ricardo

IMPLEMENTAÇÕES =====================================================
1 - Adição do campo Versao na tabela cabecalhoPedidoVenda

2 - Criação de post 'item' para a controller itemPedidoVenda para inserir item a item

3 - Validação no insert post itemPedidoVenda verificando se existem itens duplicado seja no momento da inclusao ou no banco


================================================================**/

namespace PortalGrupoAlyne.Migrations
{
    public partial class VersaoNoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Versao",
                table: "CabecalhoPedidoVenda",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 62, 254, 71, 58, 27, 26, 79, 12, 194, 54, 163, 17, 77, 209, 73, 100, 246, 64, 225, 98, 158, 58, 173, 60, 1, 162, 13, 119, 234, 74, 115, 137, 57, 31, 174, 204, 82, 247, 83, 198, 27, 186, 91, 190, 224, 29, 156, 81, 39, 119, 38, 31, 92, 53, 68, 252, 146, 228, 139, 225, 80, 22, 118, 116 }, new byte[] { 8, 160, 189, 181, 66, 15, 69, 33, 246, 219, 75, 4, 117, 52, 89, 135, 0, 248, 191, 147, 180, 169, 139, 151, 204, 209, 89, 49, 172, 244, 18, 106 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Versao",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 20, 140, 253, 177, 214, 209, 173, 197, 201, 156, 225, 175, 109, 226, 206, 156, 22, 161, 227, 16, 51, 148, 61, 51, 174, 41, 78, 112, 118, 172, 55, 51, 240, 36, 76, 171, 15, 85, 77, 218, 229, 36, 126, 187, 57, 218, 32, 7, 245, 137, 145, 21, 56, 154, 219, 67, 40, 135, 60, 52, 199, 12, 224, 154 }, new byte[] { 140, 128, 228, 31, 185, 158, 128, 141, 152, 11, 5, 185, 39, 9, 182, 123, 36, 5, 181, 161, 6, 179, 110, 217, 211, 79, 155, 142, 210, 66, 29, 26 } });
        }
    }
}
