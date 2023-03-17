using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class AlteracaoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipPed",
                table: "CabecalhoPedidoVenda",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        //    migrationBuilder.UpdateData(
        //        table: "IntegracaoSankhya",
        //        keyColumn: "Id",
        //        keyValue: 9,
        //        column: "SqlObterSankhya",
        //        value: "SELECT FIN.CODEMP as EmpresaId\r\n	                , FIN.CODPARC as ParceiroId\r\n	                , FIN.NUNOTA as NuUnico\r\n	                , FIN.DESDOBRAMENTO as Parcela\r\n	                , CONVERT(DATE,FIN.DTNEG) as DataEmissao\r\n	                , CONVERT(DATE,FIN.DTVENC) as DataVencim\r\n	                , FIN.VLRDESDOB as Valor\r\n	\r\n	                FROM TGFFIN FIN \r\n	                JOIN TGFCAB CAB ON CAB.NUNOTA = FIN.NUNOTA\r\n                        JOIN TGFPAR PAR ON FIN.CODPARC = PAR.CODPARC\r\n	                WHERE (VLRDESDOB-(VLRBAIXA+VLRDESC)) > 0\r\n                                AND PAR.ATIVO = 'S'\r\n		                AND PROVISAO = 'N'\r\n		                AND FIN.RECDESP = 1\r\n		                AND FIN.DHBAIXA IS NULL\r\n		                AND FIN.CODTIPTIT IN (0,4)\r\n		                AND FIN.CODTIPOPER NOT IN (1020,5016,5019,5029)\r\n		                AND CONVERT(DATE,FIN.DTVENC) < convert(date,dateadd(day, -3, getdate()))\r\n		                AND FIN.CODVEND = $VendedorId \r\n		                AND FIN.CODPARC NOT IN (471,512,589,1293)");

        //    migrationBuilder.InsertData(
        //        table: "Pagina",
        //        columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
        //        values: new object[,]
        //        {
        //            { 39, 30, "fa fa-file-o", 1, "Acompanhamento de Pedidos", 3, "/acompanhamento-de-pedidos" },
        //            { 40, 30, "fa fa-file-o", 4, "Acompanhamento de Pedidos", null, "/acompanhamento-de-pedidos" }
        //        });

        //    migrationBuilder.UpdateData(
        //        table: "Usuario",
        //        keyColumn: "Id",
        //        keyValue: 1,
        //        columns: new[] { "PasswordHash", "PasswordSalt" },
        //        values: new object[] { new byte[] { 78, 42, 82, 150, 28, 96, 1, 143, 18, 67, 220, 54, 205, 70, 29, 56, 210, 146, 221, 25, 11, 228, 139, 121, 229, 48, 85, 139, 96, 122, 3, 203, 70, 32, 223, 92, 168, 105, 0, 153, 19, 101, 250, 69, 69, 151, 122, 252, 109, 87, 85, 205, 200, 206, 38, 43, 56, 250, 67, 253, 14, 5, 78, 195 }, new byte[] { 146, 251, 225, 208, 119, 177, 232, 210, 1, 13, 85, 240, 35, 229, 53, 151, 203, 214, 231, 123, 37, 58, 246, 1, 62, 155, 226, 100, 206, 252, 132, 186 } });
        //}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Pagina",
            //    keyColumn: "Id",
            //    keyValue: 39);

            //migrationBuilder.DeleteData(
            //    table: "Pagina",
            //    keyColumn: "Id",
            //    keyValue: 40);

            migrationBuilder.DropColumn(
                name: "TipPed",
                table: "CabecalhoPedidoVenda");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    column: "SqlObterSankhya",
            //    value: "SELECT FIN.CODEMP as EmpresaId\r\n	                , FIN.CODPARC as ParceiroId\r\n	                , FIN.NUNOTA as NuUnico\r\n	                , FIN.DESDOBRAMENTO as Parcela\r\n	                , CONVERT(DATE,FIN.DTNEG) as DataEmissao\r\n	                , CONVERT(DATE,FIN.DTVENC) as DataVencim\r\n	                , FIN.VLRDESDOB as Valor\r\n	\r\n	                FROM TGFFIN FIN \r\n	                JOIN TGFCAB CAB ON CAB.NUNOTA = FIN.NUNOTA\r\n	                WHERE (VLRDESDOB-(VLRBAIXA+VLRDESC)) > 0\r\n		                AND PROVISAO = 'N'\r\n		                AND FIN.RECDESP = 1\r\n		                AND FIN.DHBAIXA IS NULL\r\n		                AND FIN.CODTIPTIT IN (0,4)\r\n		                AND FIN.CODTIPOPER NOT IN (1020,5016,5019,5029)\r\n		                AND CONVERT(DATE,FIN.DTVENC) < convert(date,dateadd(day, -3, getdate()))\r\n		                AND FIN.CODVEND = $VendedorId \r\n		                AND FIN.CODPARC NOT IN (471,512,589,1293)");

            //migrationBuilder.UpdateData(
            //    table: "Usuario",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { new byte[] { 112, 215, 222, 219, 254, 3, 191, 23, 9, 94, 67, 157, 150, 116, 49, 55, 144, 91, 212, 70, 134, 135, 163, 7, 130, 24, 224, 127, 196, 102, 180, 82, 25, 243, 101, 25, 245, 238, 63, 101, 147, 193, 146, 202, 180, 119, 129, 241, 28, 244, 6, 190, 83, 10, 216, 153, 125, 111, 250, 107, 102, 49, 13, 239 }, new byte[] { 27, 196, 18, 241, 202, 54, 201, 175, 45, 255, 81, 144, 199, 196, 161, 28, 4, 156, 32, 221, 70, 201, 144, 59, 176, 99, 136, 145, 138, 235, 131, 36 } });
        }
    }
}
