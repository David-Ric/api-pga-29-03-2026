using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class TableConfiguracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SankhyaServidor = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SankhyaUsuario = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SankhyaSenha = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IntegracaoSankhya",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TabelaPortal = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChaveTabelaPortal = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SqlObterSankhya = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegracaoSankhya", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Id", "AtualizadoEm", "SankhyaSenha", "SankhyaServidor", "SankhyaUsuario" },
                values: new object[] { 1, null, "SYNC550V", "http://10.0.0.254:8280/", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IntegracaoSankhya",
                columns: new[] { "Id", "AtualizadoEm", "ChaveTabelaPortal", "SqlObterSankhya", "TabelaPortal" },
                values: new object[,]
                {
                    { 1, null, "Id", "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \r\n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\r\n                    FROM TGFVEN WHERE DTALTER > '$AtualizadoEm' AND CODVEND > 0", "Vendedor" },
                    { 2, null, "Id", "SELECT DISTINCT TPV.CODTIPVENDA Id, \r\n                        RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao,\r\n                        TPV.DHALTER AtualizadoEm\r\n                    FROM TGFTPV (NOLOCK) TPV\r\n                    JOIN TGFCPL (NOLOCK) CPL ON CPL.SUGTIPNEGSAID = TPV.CODTIPVENDA\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = CPL.CODPARC AND PAR.CLIENTE = 'S'\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC AND PAEM.CODEMP = 1		\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)\r\n                    WHERE TPV.CODTIPVENDA > 0\r\n                    AND DHALTER > '$AtualizadoEm'\r\n                    ORDER BY 1", "TipoNegociacao" },
                    { 3, null, "Id", "SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, \r\n                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, \r\n                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, \r\n                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, \r\n                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,\r\n                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,\r\n                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, \r\n                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, \r\n                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm\r\n                    FROM TGFPAR (NOLOCK) PAR\r\n					JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)                    \r\n                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\r\n                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\r\n                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\r\n                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\r\n                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\r\n                    WHERE PAR.DTALTER > '$AtualizadoEm'\r\n                    AND PAR.CLIENTE = 'S' AND PAR.CODPARC > 0 AND PAR.CODVEND > 0", "Parceiro" },
                    { 4, null, "Id", "SELECT CODGRUPOPROD Id, \r\n                        RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome\r\n                    FROM sankhya.TGFGRU (NOLOCK)\r\n                    WHERE ANALITICO = 'S'", "GrupoProduto" },
                    { 5, null, "Id", "SELECT PRO.CODPROD Id, \r\n                        PRO.DESCRPROD Nome, \r\n                        PRO.CODGRUPOPROD GrupoProdutoId, \r\n                        PRO.DTALTER AtualizadoEm,\r\n                        PRO.CODVOL TipoUnid,\r\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\r\n                        ISNULL(VOA.QUANTIDADE,1) Conv\r\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\r\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\r\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\r\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')\r\n                    AND PRO.DTALTER > '$AtualizadoEm'", "Produto" },
                    { 6, null, "Id", "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \r\n                    FROM TGFNTA (NOLOCK) NTA\r\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1) AND VEN.TIPVEND = 'R'\r\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \r\n                    ORDER BY 1", "TabelaPreco" },
                    { 7, null, "TabelaPrecoId,IdProd", "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \r\n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\r\n                    FROM TGFTAB TAB\r\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\r\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\r\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\r\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \r\n                                            FROM TGFNTA (NOLOCK) NTA\r\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1) AND VEN.TIPVEND = 'R' \r\n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\r\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\r\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\r\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\r\n                    AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\r\n                    ORDER BY TAB.CODTAB, PRO.CODPROD", "ItemTabela" },
                    { 8, null, "ParceiroId,EmpresaId,TabelaPrecoId", "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\r\n                    FROM TGFPAR (NOLOCK) PAR \r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.TIPVEND = 'R' AND VEN.CODVEND NOT IN (0,1)", "TabelaPrecoParceiro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "IntegracaoSankhya");
        }
    }
}
