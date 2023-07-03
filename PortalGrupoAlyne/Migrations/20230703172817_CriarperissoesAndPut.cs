using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class CriarperissoesAndPut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 1,
                column: "SqlObterSankhya",
                value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \r\n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\r\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId ");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 2,
                column: "SqlObterSankhya",
                value: "SELECT CPL.SUGTIPNEGSAID Id\r\n                        , RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao\r\n                        , TPV.DHALTER AtualizadoEm\r\n					FROM TGFCPL CPL\r\n					JOIN TGFTPV TPV ON TPV.CODTIPVENDA = CPL.SUGTIPNEGSAID\r\n					JOIN TGFPAR PAR ON PAR.CODPARC = CPL.CODPARC\r\n					WHERE PAR.CODVEND = $VendedorId\r\n						AND PAR.ATIVO = 'S'\r\n						AND PAR.CLIENTE = 'S'\r\n					GROUP BY CPL.SUGTIPNEGSAID \r\n						, RTRIM(LTRIM(TPV.DESCRTIPVENDA))\r\n						, TPV.DHALTER");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 3,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC AS Id,\r\n    REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') AS Nome,\r\n    PAR.TIPPESSOA AS TipoPessoa,\r\n    REPLACE(PAR.NOMEPARC, CHAR(39),'') AS NomeFantasia,\r\n    PAR.CGC_CPF AS Cnpj_Cpf,\r\n    ISNULL(PAR.EMAIL, '') AS Email,\r\n    ISNULL(PAR.TELEFONE, '') AS Fone,\r\n    PAR.CODTIPPARC AS Canal,\r\n    REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND, ''), CHAR(39), '') AS Endereco,\r\n    REPLACE(ISNULL(BAI.NOMEBAI, ''), CHAR(39), '') AS Bairro,\r\n    REPLACE(CID.NOMECID, CHAR(39), '') AS Municipio,\r\n    UFS.UF AS UF,\r\n    PAR.ATIVO AS Status,\r\n    ISNULL(CPL.SUGTIPNEGSAID, 0) AS TipoNegociacao,\r\n    PAR.CODVEND AS VendedorId,\r\n    PAR.DTALTER AS AtualizadoEm,\r\n    ISNULL(PAR.LIMCRED,0) as LC,\r\n    ISNULL(PAR.LIMCRED, 0) - ISNULL(PED.VLRPED, 0) - ISNULL(FIN.VLRTIT, 0) AS SC\r\nFROM \r\n    TGFPAR (NOLOCK) PAR\r\n    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND =  $VendedorId\r\n    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\r\n    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\r\n    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\r\n    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\r\n    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\r\n    LEFT JOIN (\r\n        SELECT \r\n            CAB.CODPARC,\r\n            SUM(((ITE.QTDNEG-ITE.QTDENTREGUE) * VLRUNIT)) AS VLRPED\r\n        FROM \r\n            TGFITE ITE \r\n            JOIN TGFCAB CAB ON CAB.NUNOTA = ITE.NUNOTA\r\n        WHERE \r\n            (ITE.QTDNEG-ITE.QTDENTREGUE) > 0\r\n            AND ITE.PENDENTE = 'S'\r\n        GROUP BY \r\n            CAB.CODPARC\r\n    ) PED ON PED.CODPARC = PAR.CODPARC\r\n    LEFT JOIN (\r\n        SELECT \r\n            CAB.CODPARC,\r\n            SUM(FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA) AS VLRTIT\r\n        FROM \r\n            TGFCAB CAB\r\n            JOIN TGFFIN FIN ON FIN.NUNOTA = CAB.NUNOTA\r\n        WHERE \r\n            CAB.TIPMOV = 'V'\r\n            AND FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA > 0\r\n            AND FIN.PROVISAO <> 'S'\r\n            AND ISNULL(FIN.NURENEG, 0) = 0\r\n        GROUP BY \r\n            CAB.CODPARC\r\n    ) FIN ON FIN.CODPARC = PAR.CODPARC\r\nWHERE \r\n    PAR.CODPARC > 0\r\n    AND PAR.CODVEND > 0\r\n    AND PAR.CLIENTE = 'S'\r\n    AND PAR.CODVEND =  $VendedorId");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 6,
                column: "SqlObterSankhya",
                value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \r\n                    FROM TGFNTA (NOLOCK) NTA\r\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                            AND VEN.CODVEND = $VendedorId \r\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \r\n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 7,
                column: "SqlObterSankhya",
                value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \r\n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\r\n                    FROM TGFTAB TAB\r\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\r\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\r\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\r\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \r\n                                            FROM TGFNTA (NOLOCK) NTA\r\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                                                    AND VEN.CODVEND = $VendedorId  \r\n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\r\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\r\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\r\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\r\n                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\r\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 8,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\r\n                     FROM TGFPAR (NOLOCK) PAR\r\n                     JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\r\n                     JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND\r\n                                             AND VEN.CODVEND = $VendedorId \r\n                     WHERE PAR.CLIENTE = 'S' \r\n                     AND PAR.CODPARC > 0 \r\n                     AND PAR.CODVEND > 0\r\n                     AND PAR.ATIVO = 'S'");

            //migrationBuilder.InsertData(
            //    table: "IntegracaoSankhya",
            //    columns: new[] { "Id", "AtualizadoEm", "ChaveTabelaPortal", "SqlObterSankhya", "TabelaPortal" },
            //    values: new object[] { 10, null, "EmpresaId,ParceiroId,IdProd", "Select AD.CODEMP as EmpresaId \r\n	                 , AD.CODPARC as ParceiroId \r\n	                 , EXC.CODPROD as IdProd\r\n	                 , EXC.VLRVENDA as Preco\r\n	 \r\n	                 FROM AD_TABCLI AD \r\n	                 JOIN TGFPAR PAR ON PAR.CODPARC = AD.CODPARC \r\n	                 JOIN TGFEXC EXC ON EXC.NUTAB = AD.CODTAB \r\n	                 WHERE PAR.CODVEND = $VendedorId", "TabelaPrecoAdicional" });

            migrationBuilder.InsertData(
                table: "MenuPermissao",
                columns: new[] { "Id", "Codigo", "GrupoUsuarioId", "Nome", "UsuarioId" },
                values: new object[] { 200, 7, 5, "Consultas", null });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 200, 37, "fa fa-money", 4, "Acompanhamento Vendas", null, "/acompanhamento-vendas" },
                    { 201, 38, "", 1, "Sessões em uso", 4, "/sessoes-em-uso" },
                    { 202, 39, "", 1, "Log Ações", 4, "/log-acoes" }
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 4, 195, 138, 200, 182, 75, 121, 78, 5, 180, 223, 96, 179, 91, 83, 213, 249, 71, 113, 14, 94, 201, 181, 13, 53, 42, 166, 206, 94, 135, 163, 223, 127, 102, 173, 94, 66, 51, 46, 102, 137, 26, 4, 234, 8, 192, 139, 219, 221, 76, 154, 115, 28, 49, 241, 161, 10, 254, 51, 14, 176, 166, 26, 219 }, new byte[] { 254, 205, 241, 69, 108, 139, 163, 107, 112, 127, 48, 169, 34, 11, 96, 163, 180, 219, 3, 137, 235, 150, 242, 4, 138, 197, 83, 193, 105, 169, 28, 169 } });

            migrationBuilder.InsertData(
                table: "PaginaPermissao",
                columns: new[] { "Id", "Codigo", "GrupoUsuarioId", "MenuPermissaoId", "Nome", "SubMenuPermissaoId", "UsuarioId" },
                values: new object[] { 200, 37, 5, 200, "Acompanhamento Vendas", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "PaginaPermissao",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "MenuPermissao",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 1,
                column: "SqlObterSankhya",
                value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \r\n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\r\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId AND DTALTER > '$AtualizadoEm'");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 2,
                column: "SqlObterSankhya",
                value: "SELECT DISTINCT TPV.CODTIPVENDA Id, \r\n                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,\r\n                        TPV.DHALTER AtualizadoEm\r\n                    FROM TGFTPV (NOLOCK) TPV\r\n                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \r\n                                            AND VEN.CODVEND = $VendedorId\r\n                    WHERE TPV.CODTIPVENDA > 0\r\n                    AND DHALTER > '$AtualizadoEm'\r\n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 3,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, \r\n                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, \r\n                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, \r\n                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, \r\n                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,\r\n                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,\r\n                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, \r\n                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, \r\n                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm\r\n                        , ISNULL(PAR.LIMCRED,0) as LC\r\n                    FROM TGFPAR (NOLOCK) PAR\r\n					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \r\n                                            AND VEN.CODVEND = $VendedorId\r\n                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\r\n                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\r\n                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\r\n                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\r\n                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\r\n                    WHERE PAR.CLIENTE = 'S' \r\n                    AND PAR.CODPARC > 0 \r\n                    AND PAR.CODVEND > 0\r\n                    AND PAR.ATIVO = 'S'");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 6,
                column: "SqlObterSankhya",
                value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \r\n                    FROM TGFNTA (NOLOCK) NTA\r\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                            AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R'\r\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \r\n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 7,
                column: "SqlObterSankhya",
                value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \r\n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\r\n                    FROM TGFTAB TAB\r\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\r\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\r\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\r\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \r\n                                            FROM TGFNTA (NOLOCK) NTA\r\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                                                    AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R' \r\n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\r\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\r\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\r\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\r\n                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\r\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 8,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\r\n                    FROM TGFPAR (NOLOCK) PAR\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND\r\n                                            AND VEN.CODVEND = $VendedorId \r\n                                            AND VEN.TIPVEND = 'R'\r\n                    WHERE PAR.CLIENTE = 'S' \r\n                    AND PAR.CODPARC > 0 \r\n                    AND PAR.CODVEND > 0\r\n                    AND PAR.ATIVO = 'S'");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 239, 146, 186, 89, 128, 146, 176, 34, 39, 48, 102, 100, 103, 70, 2, 56, 229, 71, 236, 227, 130, 102, 35, 206, 76, 72, 11, 8, 183, 65, 146, 221, 69, 155, 244, 146, 169, 154, 162, 115, 26, 225, 50, 4, 227, 27, 164, 217, 38, 251, 94, 86, 236, 122, 210, 32, 171, 170, 188, 184, 125, 207, 126 }, new byte[] { 141, 245, 104, 24, 147, 111, 129, 191, 38, 151, 228, 205, 71, 201, 118, 31, 101, 14, 238, 151, 88, 208, 176, 28, 105, 89, 48, 121, 251, 28, 64, 1 } });
        }
    }
}
