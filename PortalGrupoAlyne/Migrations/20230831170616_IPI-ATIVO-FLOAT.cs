using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

/**===============================================================

Titulo:IPI-ATIVO-FLOAT.CS
Data: 31/08/2023
Versão: 1.1.015
Autor: David Ricardo

IMPLEMENTAÇÕES =====================================================
1 - Adição do campo AliIpi (alícota de ipi) na tabela Produto

2 - Remoção so campo CodIpi na tabela Produto

3 - Alteração do SqLro gergistro produto na tabela IntegracaoSankhya

4 - Alteração do formato dos campos decimal pata float nas tabelas 

5 - Adição do campo ativo na tabela CabecalhoPedidoVenda

    CabecalhoPedidoVenda e ItemPedidoVenda

6 - Filtragem dos get ca controller CabecalhoPedidoVendaController.cd
    por cabecalhos com a prop ativo !="N"

================================================================**/


namespace PortalGrupoAlyne.Migrations
{
    public partial class IPIATIVOFLOAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodIpi",
                table: "Produto");

            migrationBuilder.AddColumn<float>(
                name: "AliIpi",
                table: "Produto",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ValUnit",
                table: "ItemPedidoVenda",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ValTotal",
                table: "ItemPedidoVenda",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Quant",
                table: "ItemPedidoVenda",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Valor",
                table: "CabecalhoPedidoVenda",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ativo",
                table: "CabecalhoPedidoVenda",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 5,
                column: "SqlObterSankhya",
                value: "SELECT PRO.CODPROD Id, \n                        substring(PRO.DESCRPROD,1,60) as Nome,  \n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\n                        PRO.DTALTER AtualizadoEm,\n                        PRO.CODVOL TipoUnid,\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\n                        ISNULL(VOA.QUANTIDADE,1) Conv,\n                        isnull(IPI.PERCENTUAL,0) as AliIpi\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 20, 140, 253, 177, 214, 209, 173, 197, 201, 156, 225, 175, 109, 226, 206, 156, 22, 161, 227, 16, 51, 148, 61, 51, 174, 41, 78, 112, 118, 172, 55, 51, 240, 36, 76, 171, 15, 85, 77, 218, 229, 36, 126, 187, 57, 218, 32, 7, 245, 137, 145, 21, 56, 154, 219, 67, 40, 135, 60, 52, 199, 12, 224, 154 }, new byte[] { 140, 128, 228, 31, 185, 158, 128, 141, 152, 11, 5, 185, 39, 9, 182, 123, 36, 5, 181, 161, 6, 179, 110, 217, 211, 79, 155, 142, 210, 66, 29, 26 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliIpi",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "CabecalhoPedidoVenda");

            migrationBuilder.AddColumn<int>(
                name: "CodIpi",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValUnit",
                table: "ItemPedidoVenda",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValTotal",
                table: "ItemPedidoVenda",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quant",
                table: "ItemPedidoVenda",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "CabecalhoPedidoVenda",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 5,
                column: "SqlObterSankhya",
                value: "SELECT PRO.CODPROD Id, \n                        PRO.DESCRPROD Nome, \n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\n                        PRO.DTALTER AtualizadoEm,\n                        PRO.CODVOL TipoUnid,\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\n                        ISNULL(VOA.QUANTIDADE,1) Conv\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 172, 192, 223, 44, 240, 224, 75, 212, 52, 208, 77, 173, 96, 211, 111, 177, 107, 219, 115, 227, 213, 28, 135, 232, 175, 211, 232, 252, 252, 172, 122, 82, 172, 96, 23, 146, 174, 4, 131, 208, 101, 203, 140, 52, 124, 52, 69, 251, 46, 124, 204, 157, 20, 54, 111, 166, 52, 228, 178, 168, 168, 173, 55, 247 }, new byte[] { 145, 171, 220, 21, 248, 205, 77, 52, 86, 88, 147, 146, 184, 233, 206, 155, 180, 245, 1, 132, 101, 137, 106, 236, 128, 137, 231, 148, 152, 4, 95, 151 } });
        }
    }
}
