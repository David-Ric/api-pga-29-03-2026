using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class Ipiproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodIpi",
                table: "Produto",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PRO.CODPROD Id, \n                        PRO.DESCRPROD Nome, \n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\n                        PRO.DTALTER AtualizadoEm,\n                        PRO.CODVOL TipoUnid,\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\n                        ISNULL(VOA.QUANTIDADE,1) Conv\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 172, 192, 223, 44, 240, 224, 75, 212, 52, 208, 77, 173, 96, 211, 111, 177, 107, 219, 115, 227, 213, 28, 135, 232, 175, 211, 232, 252, 252, 172, 122, 82, 172, 96, 23, 146, 174, 4, 131, 208, 101, 203, 140, 52, 124, 52, 69, 251, 46, 124, 204, 157, 20, 54, 111, 166, 52, 228, 178, 168, 168, 173, 55, 247 }, new byte[] { 145, 171, 220, 21, 248, 205, 77, 52, 86, 88, 147, 146, 184, 233, 206, 155, 180, 245, 1, 132, 101, 137, 106, 236, 128, 137, 231, 148, 152, 4, 95, 151 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodIpi",
                table: "Produto");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PRO.CODPROD Id, \n                        PRO.DESCRPROD Nome, \n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\n                        PRO.DTALTER AtualizadoEm,\n                        PRO.CODVOL TipoUnid,\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\n                        ISNULL(VOA.QUANTIDADE,1) Conv\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')\n                    AND PRO.DTALTER > '$AtualizadoEm'");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 6, 123, 35, 152, 49, 2, 191, 85, 119, 193, 147, 221, 160, 102, 196, 245, 201, 142, 170, 111, 34, 54, 113, 139, 111, 152, 168, 15, 97, 23, 144, 67, 30, 171, 129, 142, 236, 137, 206, 139, 251, 39, 183, 210, 200, 100, 82, 139, 164, 148, 94, 252, 197, 19, 96, 39, 235, 146, 216, 150, 255, 23, 95, 49 }, new byte[] { 210, 236, 193, 64, 2, 93, 60, 242, 50, 0, 120, 92, 91, 25, 75, 226, 252, 237, 83, 101, 130, 196, 211, 25, 249, 64, 224, 228, 118, 169, 161, 12 } });
        }
    }
}
