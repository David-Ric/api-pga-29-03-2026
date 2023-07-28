using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGrupoAlyne.Migrations
{
    public partial class etiquetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeTxt = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sql = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EtiqParam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DescParam = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtiquetaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtiqParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtiqParam_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "SqlObterSankhya",
            //    value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId ");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    column: "SqlObterSankhya",
            //    value: "SELECT CPL.SUGTIPNEGSAID Id\n                        , RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao\n                        , TPV.DHALTER AtualizadoEm\n					FROM TGFCPL CPL\n					JOIN TGFTPV TPV ON TPV.CODTIPVENDA = CPL.SUGTIPNEGSAID\n					JOIN TGFPAR PAR ON PAR.CODPARC = CPL.CODPARC\n					WHERE PAR.CODVEND = $VendedorId\n						AND PAR.ATIVO = 'S'\n						AND PAR.CLIENTE = 'S'\n					GROUP BY CPL.SUGTIPNEGSAID \n						, RTRIM(LTRIM(TPV.DESCRTIPVENDA))\n						, TPV.DHALTER");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PAR.CODPARC AS Id,\n    REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') AS Nome,\n    PAR.TIPPESSOA AS TipoPessoa,\n    REPLACE(PAR.NOMEPARC, CHAR(39),'') AS NomeFantasia,\n    PAR.CGC_CPF AS Cnpj_Cpf,\n    ISNULL(PAR.EMAIL, '') AS Email,\n    ISNULL(PAR.TELEFONE, '') AS Fone,\n    PAR.CODTIPPARC AS Canal,\n    REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND, ''), CHAR(39), '') AS Endereco,\n    REPLACE(ISNULL(BAI.NOMEBAI, ''), CHAR(39), '') AS Bairro,\n    REPLACE(CID.NOMECID, CHAR(39), '') AS Municipio,\n    UFS.UF AS UF,\n    PAR.ATIVO AS Status,\n    ISNULL(CPL.SUGTIPNEGSAID, 0) AS TipoNegociacao,\n    PAR.CODVEND AS VendedorId,\n    PAR.DTALTER AS AtualizadoEm,\n    ISNULL(PAR.LIMCRED,0) as LC,\n    ISNULL(PAR.LIMCRED, 0) - ISNULL(PED.VLRPED, 0) - ISNULL(FIN.VLRTIT, 0) AS SC\nFROM \n    TGFPAR (NOLOCK) PAR\n    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND =  $VendedorId\n    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\n    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\n    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\n    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\n    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\n    LEFT JOIN (\n        SELECT \n            CAB.CODPARC,\n            SUM(((ITE.QTDNEG-ITE.QTDENTREGUE) * VLRUNIT)) AS VLRPED\n        FROM \n            TGFITE ITE \n            JOIN TGFCAB CAB ON CAB.NUNOTA = ITE.NUNOTA\n        WHERE \n            (ITE.QTDNEG-ITE.QTDENTREGUE) > 0\n            AND ITE.PENDENTE = 'S'\n        GROUP BY \n            CAB.CODPARC\n    ) PED ON PED.CODPARC = PAR.CODPARC\n    LEFT JOIN (\n        SELECT \n            CAB.CODPARC,\n            SUM(FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA) AS VLRTIT\n        FROM \n            TGFCAB CAB\n            JOIN TGFFIN FIN ON FIN.NUNOTA = CAB.NUNOTA\n        WHERE \n            CAB.TIPMOV = 'V'\n            AND FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA > 0\n            AND FIN.PROVISAO <> 'S'\n            AND ISNULL(FIN.NURENEG, 0) = 0\n        GROUP BY \n            CAB.CODPARC\n    ) FIN ON FIN.CODPARC = PAR.CODPARC\nWHERE \n    PAR.CODPARC > 0\n    AND PAR.CODVEND > 0\n    AND PAR.CLIENTE = 'S'\n    AND PAR.CODVEND =  $VendedorId");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    column: "SqlObterSankhya",
            //    value: "SELECT convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) Id, \n                    RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome\n                    FROM sankhya.TGFGRU (NOLOCK)\n                    WHERE ANALITICO = 'S'\n                    and SUBSTRING(RTRIM(CODGRUPOPROD),1,3) = '120'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PRO.CODPROD Id, \n                        PRO.DESCRPROD Nome, \n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\n                        PRO.DTALTER AtualizadoEm,\n                        PRO.CODVOL TipoUnid,\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\n                        ISNULL(VOA.QUANTIDADE,1) Conv\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')\n                    AND PRO.DTALTER > '$AtualizadoEm'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    column: "SqlObterSankhya",
            //    value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \n                    FROM TGFNTA (NOLOCK) NTA\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \n                                            AND VEN.CODVEND = $VendedorId \n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \n                    ORDER BY 1");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    column: "SqlObterSankhya",
            //    value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\n                    FROM TGFTAB TAB\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \n                                            FROM TGFNTA (NOLOCK) NTA\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \n                                                                    AND VEN.CODVEND = $VendedorId  \n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\n                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\n                     FROM TGFPAR (NOLOCK) PAR\n                     JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\n                     JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND\n                                             AND VEN.CODVEND = $VendedorId \n                     WHERE PAR.CLIENTE = 'S' \n                     AND PAR.CODPARC > 0 \n                     AND PAR.CODVEND > 0\n                     AND PAR.ATIVO = 'S'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    column: "SqlObterSankhya",
            //    value: "SELECT FIN.CODEMP as EmpresaId\n	                , FIN.CODPARC as ParceiroId\n	                , FIN.NUNOTA as NuUnico\n	                , FIN.DESDOBRAMENTO as Parcela\n	                , CONVERT(DATE,FIN.DTNEG) as DataEmissao\n	                , CONVERT(DATE,FIN.DTVENC) as DataVencim\n	                , FIN.VLRDESDOB as Valor\n	\n	                FROM TGFFIN FIN \n	                JOIN TGFCAB CAB ON CAB.NUNOTA = FIN.NUNOTA\n                        JOIN TGFPAR PAR ON FIN.CODPARC = PAR.CODPARC\n	                WHERE (VLRDESDOB-(VLRBAIXA+VLRDESC)) > 0\n                                AND PAR.ATIVO = 'S'\n		                AND PROVISAO = 'N'\n		                AND FIN.RECDESP = 1\n		                AND FIN.DHBAIXA IS NULL\n		                AND FIN.CODTIPTIT IN (0,4)\n		                AND FIN.CODTIPOPER NOT IN (1020,5016,5019,5029)\n		                AND CONVERT(DATE,FIN.DTVENC) < convert(date,dateadd(day, -3, getdate()))\n		                AND FIN.CODVEND = $VendedorId \n		                AND FIN.CODPARC NOT IN (471,512,589,1293)");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 10,
            //    column: "SqlObterSankhya",
            //    value: "Select AD.CODEMP as EmpresaId \n	                 , AD.CODPARC as ParceiroId \n	                 , EXC.CODPROD as IdProd\n	                 , EXC.VLRVENDA as Preco\n	 \n	                 FROM AD_TABCLI AD \n	                 JOIN TGFPAR PAR ON PAR.CODPARC = AD.CODPARC \n	                 JOIN TGFEXC EXC ON EXC.NUTAB = AD.CODTAB \n	                 WHERE PAR.CODVEND = $VendedorId");

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codigo", "Icon", "MenuId", "Nome", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 203, 40, "", 1, "Etiquetas", 4, "/etiquetas" },
                    { 204, 40, "", 5, "Etiquetas", null, "/etiquetas" }
                });

            //migrationBuilder.UpdateData(
            //    table: "Usuario",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { new byte[] { 41, 249, 174, 169, 91, 95, 99, 106, 60, 22, 178, 150, 216, 241, 218, 88, 123, 208, 32, 86, 128, 186, 246, 189, 133, 28, 46, 144, 46, 45, 29, 33, 53, 32, 167, 160, 97, 167, 97, 193, 183, 175, 54, 215, 117, 236, 208, 141, 63, 200, 33, 9, 75, 78, 15, 135, 234, 139, 239, 136, 59, 99, 239, 145 }, new byte[] { 243, 35, 209, 150, 196, 40, 203, 40, 78, 157, 43, 225, 190, 240, 105, 220, 14, 143, 229, 92, 147, 42, 234, 184, 248, 164, 215, 162, 44, 42, 73, 157 } });

            migrationBuilder.CreateIndex(
                name: "IX_EtiqParam_EtiquetaId",
                table: "EtiqParam",
                column: "EtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtiqParam");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Pagina",
                keyColumn: "Id",
                keyValue: 204);

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "SqlObterSankhya",
            //    value: "SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, \r\n                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm\r\n                    FROM TGFVEN VEN WHERE VEN.CODVEND = $VendedorId ");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    column: "SqlObterSankhya",
            //    value: "SELECT CPL.SUGTIPNEGSAID Id\r\n                        , RTRIM(LTRIM(TPV.DESCRTIPVENDA)) Descricao\r\n                        , TPV.DHALTER AtualizadoEm\r\n					FROM TGFCPL CPL\r\n					JOIN TGFTPV TPV ON TPV.CODTIPVENDA = CPL.SUGTIPNEGSAID\r\n					JOIN TGFPAR PAR ON PAR.CODPARC = CPL.CODPARC\r\n					WHERE PAR.CODVEND = $VendedorId\r\n						AND PAR.ATIVO = 'S'\r\n						AND PAR.CLIENTE = 'S'\r\n					GROUP BY CPL.SUGTIPNEGSAID \r\n						, RTRIM(LTRIM(TPV.DESCRTIPVENDA))\r\n						, TPV.DHALTER");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PAR.CODPARC AS Id,\r\n    REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') AS Nome,\r\n    PAR.TIPPESSOA AS TipoPessoa,\r\n    REPLACE(PAR.NOMEPARC, CHAR(39),'') AS NomeFantasia,\r\n    PAR.CGC_CPF AS Cnpj_Cpf,\r\n    ISNULL(PAR.EMAIL, '') AS Email,\r\n    ISNULL(PAR.TELEFONE, '') AS Fone,\r\n    PAR.CODTIPPARC AS Canal,\r\n    REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND, ''), CHAR(39), '') AS Endereco,\r\n    REPLACE(ISNULL(BAI.NOMEBAI, ''), CHAR(39), '') AS Bairro,\r\n    REPLACE(CID.NOMECID, CHAR(39), '') AS Municipio,\r\n    UFS.UF AS UF,\r\n    PAR.ATIVO AS Status,\r\n    ISNULL(CPL.SUGTIPNEGSAID, 0) AS TipoNegociacao,\r\n    PAR.CODVEND AS VendedorId,\r\n    PAR.DTALTER AS AtualizadoEm,\r\n    ISNULL(PAR.LIMCRED,0) as LC,\r\n    ISNULL(PAR.LIMCRED, 0) - ISNULL(PED.VLRPED, 0) - ISNULL(FIN.VLRTIT, 0) AS SC\r\nFROM \r\n    TGFPAR (NOLOCK) PAR\r\n    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND =  $VendedorId\r\n    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID\r\n    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF\r\n    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC\r\n    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND\r\n    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI\r\n    LEFT JOIN (\r\n        SELECT \r\n            CAB.CODPARC,\r\n            SUM(((ITE.QTDNEG-ITE.QTDENTREGUE) * VLRUNIT)) AS VLRPED\r\n        FROM \r\n            TGFITE ITE \r\n            JOIN TGFCAB CAB ON CAB.NUNOTA = ITE.NUNOTA\r\n        WHERE \r\n            (ITE.QTDNEG-ITE.QTDENTREGUE) > 0\r\n            AND ITE.PENDENTE = 'S'\r\n        GROUP BY \r\n            CAB.CODPARC\r\n    ) PED ON PED.CODPARC = PAR.CODPARC\r\n    LEFT JOIN (\r\n        SELECT \r\n            CAB.CODPARC,\r\n            SUM(FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA) AS VLRTIT\r\n        FROM \r\n            TGFCAB CAB\r\n            JOIN TGFFIN FIN ON FIN.NUNOTA = CAB.NUNOTA\r\n        WHERE \r\n            CAB.TIPMOV = 'V'\r\n            AND FIN.VLRDESDOB-FIN.VLRDESC-FIN.VLRBAIXA > 0\r\n            AND FIN.PROVISAO <> 'S'\r\n            AND ISNULL(FIN.NURENEG, 0) = 0\r\n        GROUP BY \r\n            CAB.CODPARC\r\n    ) FIN ON FIN.CODPARC = PAR.CODPARC\r\nWHERE \r\n    PAR.CODPARC > 0\r\n    AND PAR.CODVEND > 0\r\n    AND PAR.CLIENTE = 'S'\r\n    AND PAR.CODVEND =  $VendedorId");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    column: "SqlObterSankhya",
            //    value: "SELECT convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) Id, \r\n                    RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome\r\n                    FROM sankhya.TGFGRU (NOLOCK)\r\n                    WHERE ANALITICO = 'S'\r\n                    and SUBSTRING(RTRIM(CODGRUPOPROD),1,3) = '120'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PRO.CODPROD Id, \r\n                        PRO.DESCRPROD Nome, \r\n                        convert(int,SUBSTRING(RTRIM(CODGRUPOPROD),2,5)) GrupoProdutoId,\r\n                        PRO.DTALTER AtualizadoEm,\r\n                        PRO.CODVOL TipoUnid,\r\n                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,\r\n                        ISNULL(VOA.QUANTIDADE,1) Conv\r\n                    FROM sankhya.TGFPRO (NOLOCK) PRO\r\n                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'\r\n                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'\r\n                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')\r\n                    AND PRO.DTALTER > '$AtualizadoEm'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    column: "SqlObterSankhya",
            //    value: "SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal \r\n                    FROM TGFNTA (NOLOCK) NTA\r\n                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                            AND VEN.CODVEND = $VendedorId \r\n                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR \r\n                    ORDER BY 1");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    column: "SqlObterSankhya",
            //    value: "SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, \r\n                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm\r\n                    FROM TGFTAB TAB\r\n                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB\r\n                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB\r\n                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD\r\n                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB \r\n                                            FROM TGFNTA (NOLOCK) NTA\r\n                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB\r\n                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC\r\n						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND \r\n                                                                    AND VEN.CODVEND = $VendedorId  \r\n                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))\r\n                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB\r\n                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())\r\n                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)\r\n                    --AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'\r\n                    ORDER BY TAB.CODTAB, PRO.CODPROD");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    column: "SqlObterSankhya",
            //    value: "SELECT PAR.CODPARC ParceiroId, PAEM.CODEMP EmpresaId, PAEM.CODTAB TabelaPrecoId\r\n                     FROM TGFPAR (NOLOCK) PAR\r\n                     JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODPARC = PAR.CODPARC\r\n                     JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND\r\n                                             AND VEN.CODVEND = $VendedorId \r\n                     WHERE PAR.CLIENTE = 'S' \r\n                     AND PAR.CODPARC > 0 \r\n                     AND PAR.CODVEND > 0\r\n                     AND PAR.ATIVO = 'S'");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    column: "SqlObterSankhya",
            //    value: "SELECT FIN.CODEMP as EmpresaId\r\n	                , FIN.CODPARC as ParceiroId\r\n	                , FIN.NUNOTA as NuUnico\r\n	                , FIN.DESDOBRAMENTO as Parcela\r\n	                , CONVERT(DATE,FIN.DTNEG) as DataEmissao\r\n	                , CONVERT(DATE,FIN.DTVENC) as DataVencim\r\n	                , FIN.VLRDESDOB as Valor\r\n	\r\n	                FROM TGFFIN FIN \r\n	                JOIN TGFCAB CAB ON CAB.NUNOTA = FIN.NUNOTA\r\n                        JOIN TGFPAR PAR ON FIN.CODPARC = PAR.CODPARC\r\n	                WHERE (VLRDESDOB-(VLRBAIXA+VLRDESC)) > 0\r\n                                AND PAR.ATIVO = 'S'\r\n		                AND PROVISAO = 'N'\r\n		                AND FIN.RECDESP = 1\r\n		                AND FIN.DHBAIXA IS NULL\r\n		                AND FIN.CODTIPTIT IN (0,4)\r\n		                AND FIN.CODTIPOPER NOT IN (1020,5016,5019,5029)\r\n		                AND CONVERT(DATE,FIN.DTVENC) < convert(date,dateadd(day, -3, getdate()))\r\n		                AND FIN.CODVEND = $VendedorId \r\n		                AND FIN.CODPARC NOT IN (471,512,589,1293)");

            //migrationBuilder.UpdateData(
            //    table: "IntegracaoSankhya",
            //    keyColumn: "Id",
            //    keyValue: 10,
            //    column: "SqlObterSankhya",
            //    value: "Select AD.CODEMP as EmpresaId \r\n	                 , AD.CODPARC as ParceiroId \r\n	                 , EXC.CODPROD as IdProd\r\n	                 , EXC.VLRVENDA as Preco\r\n	 \r\n	                 FROM AD_TABCLI AD \r\n	                 JOIN TGFPAR PAR ON PAR.CODPARC = AD.CODPARC \r\n	                 JOIN TGFEXC EXC ON EXC.NUTAB = AD.CODTAB \r\n	                 WHERE PAR.CODVEND = $VendedorId");

            //migrationBuilder.UpdateData(
            //    table: "Usuario",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { new byte[] { 81, 208, 34, 125, 186, 251, 203, 19, 249, 33, 238, 133, 4, 159, 213, 215, 251, 209, 37, 194, 195, 247, 38, 209, 252, 236, 120, 0, 108, 159, 41, 103, 34, 243, 8, 186, 200, 169, 74, 213, 71, 26, 137, 116, 111, 33, 190, 220, 174, 245, 240, 180, 35, 112, 110, 195, 51, 33, 98, 0, 87, 76, 120, 145 }, new byte[] { 110, 238, 106, 126, 207, 238, 196, 93, 111, 26, 2, 141, 70, 220, 77, 131, 183, 64, 59, 243, 135, 80, 77, 238, 85, 58, 133, 185, 208, 86, 87, 10 } });
        }
    }
}
