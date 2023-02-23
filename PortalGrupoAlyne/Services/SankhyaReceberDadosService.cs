using System.Text.Json;
using Dapper;
using MySqlConnector;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaReceberDadosService
    {
        static IConfiguration? _configuration;
        static DataContext? _context;
        public static async Task<Object> processar(IConfiguration configuration, DataContext context)
        {
            try
            {
                LoginResponse? result = (LoginResponse?)await SankhyaService.login();
                if (result != null && result.status == "1") //SankhyaService.getJsessionid() == null) 
                {
                    _configuration = configuration;
                    _context = context;
                    DateTime AtualizadoEm;
                    string sql;
                    // ------------------------ Vendedor --------------------------------
                    AtualizadoEm = atualizadoEm("Vendedor"); // último timestamp
                    sql = @"SELECT CODVEND Id, APELIDO Nome, ATIVO Status, ISNULL(EMAIL, '') Email, 
                    TIPVEND Tipo, CASE WHEN ATUACOMPRADOR = 'S' THEN 1 ELSE 0 END AtuaCompras, DTALTER AtualizadoEm
                    FROM TGFVEN WHERE DTALTER > '$AtualizadoEm' AND CODVEND > 0";
                    sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));
                    await AtualizarTabela(sql, "Vendedor", "Id");
                    // ------------------------ Tipo Negociacao -------------------------
                    AtualizadoEm = atualizadoEm("TipoNegociacao"); // último timestamp
                    sql = @"SELECT CODTIPVENDA Id, 
                        RTRIM(LTRIM(DESCRTIPVENDA)) Descricao,
                        DHALTER AtualizadoEm
                    FROM TGFTPV
                    WHERE CODTIPVENDA IN (
                        SELECT COND FROM (
                            SELECT CPL.SUGTIPNEGSAID COND
                                from TGFPAR PAR
                                LEFT JOIN TGFCPL CPL ON CPL.CODPARC = PAR.CODPARC
                                , TGFPAEM PAEM
                                WHERE -- PAR.DTALTER >= CONVERT(DATE,SYSDATETIME())
                                CLIENTE = 'S'
                                AND PAR.CODPARC = PAEM.CODPARC
                                AND PAEM.CODEMP = 1
                                AND CODVEND IN (
                                        SELECT CODVEND 
                                        FROM TGFVEN VEN
                                        WHERE CODVEND NOT IN (0,1)
                                        AND TIPVEND = 'R'
                                        )
                            GROUP BY CPL.SUGTIPNEGSAID
                        ) T WHERE T.COND IS NOT NULL AND T.COND <> 0
                    )
                    AND DHALTER > '$AtualizadoEm'";
                    sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));
                    await AtualizarTabela(sql, "TipoNegociacao", "Id");
                    // ------------------------ Parceiro --------------------------------
                    AtualizadoEm = atualizadoEm("Parceiro"); // último timestamp
                    sql = @"SELECT PAR.CODPARC Id, REPLACE(PAR.RAZAOSOCIAL, CHAR(39),'') Nome, 
                        PAR.TIPPESSOA TipoPessoa, REPLACE(PAR.NOMEPARC, CHAR(39),'') NomeFantasia, 
                        PAR.CGC_CPF Cnpj_Cpf, ISNULL(PAR.EMAIL,'') Email, 
                        ISNULL(PAR.TELEFONE,'') Fone, PAR.CODTIPPARC Canal, 
                        REPLACE(ISNULL(EN1.TIPO +' '+ EN1.NOMEEND,''), CHAR(39), '') Endereco,
                        REPLACE(ISNULL(BAI.NOMEBAI,''), CHAR(39),'') Bairro,
                        REPLACE(CID.NOMECID, CHAR(39),'') Municipio, UFS.UF UF, 
                        PAR.ATIVO Status, ISNULL(CPL.SUGTIPNEGSAID,0) TipoNegociacao, 
                        PAR.CODVEND VendedorId, PAR.DTALTER AtualizadoEm
                    FROM TGFPAR (NOLOCK) PAR
                    JOIN TSICID (NOLOCK) CID ON CID.CODCID = PAR.CODCID
                    JOIN TSIUFS (NOLOCK) UFS ON UFS.CODUF = CID.UF
                    LEFT JOIN TGFCPL (NOLOCK) CPL ON CPL.CODPARC = PAR.CODPARC
                    LEFT JOIN TSIEND (NOLOCK) EN1 ON EN1.CODEND = PAR.CODEND
                    LEFT JOIN TSIBAI (NOLOCK) BAI ON BAI.CODBAI = PAR.CODBAI
                    WHERE PAR.DTALTER > '$AtualizadoEm'
                    AND PAR.CLIENTE = 'S' AND PAR.CODPARC > 0 AND PAR.CODVEND > 0";
                    sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));
                    await AtualizarTabela(sql, "Parceiro", "Id");
                    // ------------------------ Grupo Produto ---------------------------
                    sql = @"SELECT CODGRUPOPROD Id, 
                        RTRIM(LTRIM(REPLACE(ISNULL(DESCRGRUPOPROD,''), CHAR(39),''))) Nome
                    FROM sankhya.TGFGRU (NOLOCK)
                    WHERE ANALITICO = 'S'";
                    await AtualizarTabela(sql, "GrupoProduto", "Id");
                    // ------------------------ Produto --------------------------------
                    AtualizadoEm = atualizadoEm("Produto"); // último timestamp
                    sql = @"SELECT PRO.CODPROD Id, 
                        PRO.DESCRPROD Nome, 
                        PRO.CODGRUPOPROD GrupoProdutoId, 
                        PRO.DTALTER AtualizadoEm,
                        PRO.CODVOL TipoUnid,
                        ISNULL(VOA.CODVOL,'UN') TipoUnid2,
                        ISNULL(VOA.QUANTIDADE,1) Conv
                    FROM sankhya.TGFPRO (NOLOCK) PRO
                    LEFT JOIN sankhya.TGFVOA (NOLOCK) VOA ON VOA.CODPROD = PRO.CODPROD AND VOA.ATIVO = 'S' AND VOA.AD_UNCOM = 'S'
                    LEFT JOIN sankhya.TGFIPI (NOLOCK) IPI ON IPI.CODIPI = PRO.CODIPI AND VOA.ATIVO = 'S'
                    WHERE PRO.CODPROD <> 0 AND PRO.USOPROD IN ('V','R')
                    AND PRO.DTALTER > '$AtualizadoEm'";
                    sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));
                    await AtualizarTabela(sql, "Produto", "Id");
                    // ------------------------ Tabela de Preco -------------------------
                    AtualizadoEm = atualizadoEm("TabelaPreco"); // último timestamp
                    sql = @"SELECT NTA.CODTAB Id, 1 Codigo, RTRIM(LTRIM(NTA.NOMETAB)) Descricao, TAB.DTVIGOR DataInicial, '2070-01-01 01:01:01' DataFinal 
                    FROM TGFNTA (NOLOCK) NTA
                    JOIN (SELECT CODTAB, MAX(DTVIGOR) DTVIGOR FROM TGFTAB (NOLOCK) GROUP BY CODTAB) TAB ON TAB.CODTAB = NTA.CODTAB
                    JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB
                    JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC
                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1)  AND VEN.TIPVEND = 'R'
                    WHERE NTA.ATIVO = 'S'
                    GROUP BY NTA.CODTAB,TAB.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)),TAB.DTVIGOR 
                    ORDER BY 1";
                    await AtualizarTabela(sql, "TabelaPreco", "Id");
                    // ------------------------ Item da Tabela de Preco -----------------
                    AtualizadoEm = atualizadoEm("ItemTabela"); // último timestamp
                    sql = @"SELECT TAB.CODTAB TabelaPrecoId, EXC.CODPROD IdProd, EXC.VLRVENDA Preco, 
                    ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') AtualizadoEm
                    FROM TGFTAB TAB
                    JOIN TGFNTA NTA ON NTA.CODTAB = TAB.CODTAB AND NTA.ATIVO='S'
                    JOIN TGFEXC EXC ON EXC.NUTAB = TAB.NUTAB
                    JOIN TGFPRO PRO ON PRO.CODPROD = EXC.CODPROD
                    WHERE TAB.CODTAB IN (	SELECT NTA.CODTAB 
                                            FROM TGFNTA (NOLOCK) NTA
                                            JOIN TGFPAEM (NOLOCK) PAEM ON PAEM.CODTAB = NTA.CODTAB
                                            JOIN TGFPAR (NOLOCK) PAR ON PAR.CODPARC = PAEM.CODPARC
						                    JOIN TGFVEN (NOLOCK) VEN ON VEN.CODVEND = PAR.CODVEND AND VEN.CODVEND NOT IN (0,1) AND VEN.TIPVEND = 'R' 
                                            WHERE NTA.ATIVO = 'S'
                                            GROUP BY NTA.CODTAB,RTRIM(LTRIM(NTA.NOMETAB)))
                    AND EXC.NUTAB = (SELECT TOP 1 NUTAB FROM TGFTAB WHERE CODTAB = TAB.CODTAB
                                    AND CONVERT(DATE,DTVIGOR) <= CONVERT(DATE,GETDATE())
                                    ORDER BY EXC.CODPROD, DTVIGOR DESC)
                    AND ISNULL(EXC.AD_DTALTER, '1970-01-01 01:01:02') > '$AtualizadoEm'
                    ORDER BY TAB.CODTAB, PRO.CODPROD";
                    sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));
                    await AtualizarTabela(sql, "ItemTabela", "TabelaPrecoId,IdProd");
                    // ------------------------ Logout ----------------------------------
                    await SankhyaService.logout();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                return "error: " + e.Message;
            }
            return "Sucesso";
        }

        public static async Task<String> AtualizarTabela(string sql, string table, string key)
        {
            try
            {
                Console.WriteLine(sql);
                QueryResponse data = await SankhyaService.execQuery(sql);
                if (data != null)
                {
                    if (data.status == "1")
                    {
                        IList<QueryFieldsMetadata> fieldsMetadata = data.responseBody.fieldsMetadata;
                        IList<IList<Object>> rows = data.responseBody.rows;
                        if (rows != null)
                        {
                            string MySqlCon = _configuration.GetConnectionString("DefaultConnection");
                            using var con = new MySqlConnection(MySqlCon);
                            con.Open();
                            for (int i = 0; i < rows.Count; i++)
                            {
                                string where = "";
                                int qtdCol = fieldsMetadata.Count;
                                String fieldsValues = "";
                                String fields = "";
                                String values = "";
                                for (int j = 0; j < qtdCol; j++)
                                {
                                    QueryFieldsMetadata field = fieldsMetadata[j];
                                    String name = field.name;
                                    IList<Object> row = rows[i];
                                    Object value = row[j];
                                    if (field.userType == "S" && value != null)
                                        value = $"'{value.ToString().Trim()}'";
                                    if (field.userType == "H" && value != null)
                                    {
                                        DateTime dtvalue = DateTime.ParseExact(value.ToString(), "ddMMyyyy HH:mm:ss", null);
                                        value = "'" + dtvalue.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                    }
                                    fieldsValues += $"{name}={value},";
                                    fields += $"{name},";
                                    values += $"{value},";
                                    if (key != null)
                                    {
                                        String[] keydata = key.Split(',');
                                        if (keydata.Contains(name))
                                        {
                                            value = $"'{value.ToString().Trim()}'";
                                            where += $"{name} = {value} AND ";
                                        }
                                    }
                                }
                                fieldsValues = fieldsValues.Substring(0, fieldsValues.Length - 1);
                                fields = fields.Substring(0, fields.Length - 1);
                                values = values.Substring(0, values.Length - 1);
                                if (where.Count() > 0)
                                {
                                    where = where.Substring(0, where.Length - 5);
                                    String query = $"SELECT {key} FROM {table} WHERE {where}";
                                    Console.WriteLine(query);
                                    string chave = con.ExecuteScalar<string>(query);
                                    var cSql = "";
                                    if (chave != null) cSql = $"UPDATE {table} SET {fieldsValues} WHERE {where}";
                                    else cSql = $"INSERT INTO {table} ({fields}) VALUES({values})";
                                    Console.WriteLine(cSql);
                                    int resultado = con.Execute(cSql);
                                }
                            }
                            con.Close();
                        }
                    }
                    else
                    {
                        Console.WriteLine("message" + data.statusMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                throw new Exception("Falha na comunicação com o BD!" + "\n" + e.Message);
            }
            return "Sucesso";
        }

        public static DateTime atualizadoEm(string table)
        {
            DateTime AtualizadoEm = DateTime.Now;
            try
            {
                string MySqlCon = _configuration.GetConnectionString("DefaultConnection");
                using var con = new MySqlConnection(MySqlCon);
                con.Open();
                string sql = $"SELECT COALESCE(MAX(AtualizadoEm),'1970-01-01 01:01:01') AtualizadoEm FROM {table}";
                AtualizadoEm = con.ExecuteScalar<DateTime>(sql);
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.Message);
            }
            return AtualizadoEm;
        }
    }
}