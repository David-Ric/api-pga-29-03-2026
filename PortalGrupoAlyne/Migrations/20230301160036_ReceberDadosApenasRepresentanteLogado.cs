using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class ReceberDadosApenasRepresentanteLogado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 1,
                column: "SqlObterSankhya",
                value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId AND DTALTER > '$AtualizadoEm'");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 2,
                column: "SqlObterSankhya",
                value: "SELECT DISTINCT TPV.CODTIPVENDA Id, \n                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,\n                        TPV.DHALTER AtualizadoEm\n                    FROM TGFTPV (NOLOCK) TPV\n                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \n                                            AND VEN.CODVEND = $VendedorId\n                    WHERE TPV.CODTIPVENDA > 0\n                    AND DHALTER > '$AtualizadoEm'\n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 3,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, \n                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, \n                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, \n                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, \n                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,\n                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,\n                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, \n                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, \n                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm\n                    FROM TGFPAR (NOLOCK) PAR\n					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' \n                                            AND VEN.CODVEND = $VendedorId\n                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\n                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\n                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\n                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\n                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\n                    WHERE PAR.DTALTER > '$AtualizadoEm'\n                    AND PAR.CLIENTE = 'S' AND PAR.CODPARC > 0 AND PAR.CODVEND > 0");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 6,
                column: "SqlObterSankhya",
                value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \n                    FROM TGFNTA (NOLOCK) NTA\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \n                                            AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R'\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 7,
                column: "SqlObterSankhya",
                value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\n                    FROM TGFTAB TAB\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \n                                            FROM TGFNTA (NOLOCK) NTA\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \n                                                                    AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R' \n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\n                    AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 8,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\n                    FROM TGFPAR (NOLOCK) PAR \n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \n                                            AND VEN.CODVEND = $VendedorId AND VEN.TIPVEND = 'R'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 1,
                column: "SqlObterSankhya",
                value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\n                    FROM TGFVEN WHERE DTALTER > '$AtualizadoEm' AND CODVEND > 0");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 2,
                column: "SqlObterSankhya",
                value: "SELECT DISTINCT TPV.CODTIPVENDA Id, \n                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,\n                        TPV.DHALTER AtualizadoEm\n                    FROM TGFTPV (NOLOCK) TPV\n                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)\n                    WHERE TPV.CODTIPVENDA > 0\n                    AND DHALTER > '$AtualizadoEm'\n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 3,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, \n                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, \n                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, \n                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, \n                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,\n                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,\n                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, \n                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, \n                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm\n                    FROM TGFPAR (NOLOCK) PAR\n					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)                    \n                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\n                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\n                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\n                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\n                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\n                    WHERE PAR.DTALTER > '$AtualizadoEm'\n                    AND PAR.CLIENTE = 'S' AND PAR.CODPARC > 0 AND PAR.CODVEND > 0");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 6,
                column: "SqlObterSankhya",
                value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \n                    FROM TGFNTA (NOLOCK) NTA\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1)  AND VEN.TIPVEND = 'R'\n                                       GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \n                    ORDER BY 1");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 7,
                column: "SqlObterSankhya",
                value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\n                    FROM TGFTAB TAB\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB                JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \n                                            FROM TGFNTA (NOLOCK) NTA\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1) AND VEN.TIPVEND = 'R' \n                                                                             GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\n                    AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            migrationBuilder.UpdateData(
                table: "IntegracaoSankhya",
                keyColumn: "Id",
                keyValue: 8,
                column: "SqlObterSankhya",
                value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\n                    FROM TGFPAR (NOLOCK) PAR \n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)");
        }
    }
}
