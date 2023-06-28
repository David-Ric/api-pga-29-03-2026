using System.Text;
using System.Text.Json;
using Dapper;
using Microsoft.Win32;
using MySqlConnector;
using PortalGrupoAlyne.Model;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaReceberDadosService
    {
        static IConfiguration? _configuration;

        

        public static async Task<Object> processar(IConfiguration configuration, string tabela, int vendedorId)
        {
            try
            {
                LoginResponse? result = (LoginResponse?)await SankhyaService.login(configuration);
                if (result != null && result.status == "1") //SankhyaService.getJsessionid() == null) 
                {
                    _configuration = configuration;
                    IntegracaoSankhya integracao;
                    DateTime AtualizadoEm;
                    string? sql;
                    string? chave;
                    if (tabela == "Parceiro")
                    {
                        limpaTb("Parceiro", " VendedorId = " + vendedorId);
                    }
                    if (tabela == "Titulo")
                    {
                        limpaTb("Titulo", " ParceiroId in (select id from Parceiro where VendedorId = "+ vendedorId + ")");
                    }
                    if (tabela == "TabelaPrecoParceiro")
                    {
                        limpaTb("TabelaPrecoParceiro", " ParceiroId in (select id from Parceiro where VendedorId = " + vendedorId + ")");
                    }
                    if (tabela == "ItemTabela")
                    {
                        string condicao = @"TabelaPrecoId IN 
                     (SELECT TabelaPrecoId FROM TabelaPrecoParceiro WHERE ParceiroId IN 
                     (SELECT id FROM Parceiro WHERE VendedorId = " + vendedorId + "))";

                        limpaTb("ItemTabela", condicao);
                    }
                    if (tabela == "TabelaPrecoAdicional")
                    {
                        limpaTb("TabelaPrecoAdicional", " ParceiroId in (select id from Parceiro where VendedorId = " + vendedorId + ")");
                    }
                    AtualizadoEm = atualizadoEm(tabela); // último timestamp
                    integracao = obterIntegracaoSankhya(configuration, tabela);
                    sql = integracao.SqlObterSankhya;
                    chave = integracao.ChaveTabelaPortal;
                    if (sql != null && chave != null)
                    {
                        if (vendedorId > 0)
                        {
                            sql = sql.Replace("$VendedorId", vendedorId.ToString());
                        }
                        else
                        {
                            sql = sql.Replace("VEN.CODVEND = $VendedorId AND ", "");
                        }
                        sql = sql.Replace("$AtualizadoEm", AtualizadoEm.ToString("yyyyMMdd HH:mm:ss"));



                        await AtualizarTabela(sql, tabela, chave);
                    }
                    // ------------------------ Logout ----------------------------------
                    await SankhyaService.logout(configuration);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                return "error: " + e.Message;
            }
            return "Sucesso";
        }
        public static string limpaTb(string table, string condicao)
        {
            try
            {
                string MySqlCon = _configuration.GetConnectionString("DefaultConnection");
                using var con = new MySqlConnection(MySqlCon);
                con.Open();
                string sql = $"delete FROM {table} where {condicao}";
                con.Execute(sql);
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.Message);
            }
            return "Sucesso";
        }

        public static IntegracaoSankhya obterIntegracaoSankhya(IConfiguration configuration, string tabelaPortal)
        {
            string MySqlCon = configuration.GetConnectionString("DefaultConnection");
            using var con = new MySqlConnection(MySqlCon);
            con.Open();
            IntegracaoSankhya integracaoSankhya = con.QueryFirst<IntegracaoSankhya>(
                "SELECT * FROM IntegracaoSankhya WHERE TabelaPortal=@TabelaPortal", new { TabelaPortal = tabelaPortal });
            con.Close();
            return integracaoSankhya;
        }

        public static async Task<string> AtualizarTabela(string sql, string table, string key)
        {
            List<IList<object>> registrosProcessados = new List<IList<object>>();

            try
            {
                Console.WriteLine(sql);
                QueryResponse data = await SankhyaService.executeQuery(_configuration, sql);
                if (data != null)
                {
                    if (data.status == "1")
                    {
                        IList<QueryFieldsMetadata> fieldsMetadata = data.responseBody.fieldsMetadata;
                        IList<IList<object>> rows = data.responseBody.rows;

                        if (rows != null)
                        {
                            registrosProcessados.AddRange(rows.Select(row => row.ToList()));

                            string MySqlCon = _configuration.GetConnectionString("DefaultConnection");
                            using (var con = new MySqlConnection(MySqlCon))
                            {
                                con.Open();
                                List<string> sqlStatements = new List<string>(); // Lista para armazenar as instruções SQL
                                for (int i = 0; i < rows.Count; i++)
                                {
                                    string where = "";
                                    int qtdCol = fieldsMetadata.Count;
                                    string fieldsValues = "";
                                    string fields = "";
                                    string values = "";
                                    IList<object> row = rows[i];

                                    bool hasNullFields = false;

                                    for (int j = 0; j < qtdCol; j++)
                                    {
                                        QueryFieldsMetadata field = fieldsMetadata[j];
                                        string name = field.name;
                                        object value = row[j];

                                        if ((field.userType?.Equals("I") ?? false) && value == null)
                                        {
                                            Console.WriteLine("Registro não inserido: " + string.Join(", ", row));
                                            hasNullFields = true; // Marca que há campos nulos
                                            break;
                                        }

                                        if (field.userType == "S" && value != null)
                                            value = $"'{value?.ToString().Trim()}'";
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
                                            string[] keydata = key.Split(',');
                                            if (keydata.Contains(name))
                                            {
                                                value = $"'{value?.ToString().Trim()}'";
                                                where += $"{name} = {value} AND ";
                                            }
                                        }
                                    }
                                    //if (hasNullFields) // Verifica se há campos nulos
                                    //    continue; // P
                                    fieldsValues = fieldsValues.TrimEnd(',');
                                    fields = fields.TrimEnd(',');
                                    values = values.TrimEnd(',');
                                    if (where.Count() > 0)
                                    {
                                        where = where.TrimEnd("AND ".ToCharArray()); // Corrigido
                                        string query = $"SELECT {key} FROM {table} WHERE {where}";
                                        Console.WriteLine(query);
                                        string chave = con.ExecuteScalar<string>(query);
                                        var cSql = "";
                                        if (chave != null)
                                            cSql = $"UPDATE {table} SET {fieldsValues} WHERE {where}";
                                        else
                                            cSql = $"INSERT INTO {table} ({fields}) VALUES({values})";
                                        Console.WriteLine(cSql);
                                        sqlStatements.Add(cSql); // Adiciona a instrução SQL à li
                                    }
                                }
                                foreach (var sqlStatement in sqlStatements)
                                {
                                    int resultado = con.Execute(sqlStatement);
                                }
                                con.Close();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro: " + data?.statusMessage);
                    }
                }
            }


            catch (Exception e)
            {
                
                Console.WriteLine("Erro: " + e.Message);

                string mensagemErro = "Erro de migração de dados! ";

                if (e.Message == "Field 'TabelaPrecoId' doesn't have a default value")
                {
                    mensagemErro += "Estes parceiros não possuem tabelas de preço vinculadas. O campo 'TabelaPrecoId' está vazio.";
                }
                else if (e.Message == "Cannot add or update a child row: a foreign key constraint fails (`grupoalyne`.`tabelaprecoparceiro`, CONSTRAINT `FK_TabelaPrecoParceiro_Parceiro_ParceiroId` FOREIGN KEY (`ParceiroId`) REFERENCES `parceiro` (`id`) ON DELETE CASCADE)")
                {
                    mensagemErro += "O parceiro vinculado a uma das tabelas de preço parceiro, não existe na base de dados.";
                }
                else
                {
                    mensagemErro += e.Message;
                }

                List<string> registrosErro = new List<string>();
                foreach (var registro in registrosProcessados)
                {
                    registrosErro.Add(string.Join(", ", registro.Select(value => value?.ToString())));
                }

                List<string> registrosComCamposNulos = registrosProcessados
              .Where(registro => registro.Any(value => value == null || value?.ToString() == ""))
              .Select(registro => string.Join(", ", registro.Select(value => value?.ToString())))
              .ToList();

                mensagemErro += " / Registros não processados: " + string.Join(", ", registrosComCamposNulos);

                await AtualizarTabelaPosErro(sql, table, key);

                throw new Exception(mensagemErro);
            }

            return "Sucesso";
        }

        public static async Task<string> AtualizarTabelaPosErro(string sql, string table, string key)
        {
            List<IList<object>> registrosProcessados = new List<IList<object>>();

            try
            {
                Console.WriteLine(sql);
                QueryResponse data = await SankhyaService.executeQuery(_configuration, sql);
                if (data != null)
                {
                    if (data.status == "1")
                    {
                        IList<QueryFieldsMetadata> fieldsMetadata = data.responseBody.fieldsMetadata;
                        IList<IList<object>> rows = data.responseBody.rows;

                        if (rows != null)
                        {
                            registrosProcessados.AddRange(rows.Select(row => row.ToList()));

                            string MySqlCon = _configuration.GetConnectionString("DefaultConnection");
                            using (var con = new MySqlConnection(MySqlCon))
                            {
                                con.Open();
                                List<string> sqlStatements = new List<string>(); // Lista para armazenar as instruções SQL
                                for (int i = 0; i < rows.Count; i++)
                                {
                                    string where = "";
                                    int qtdCol = fieldsMetadata.Count;
                                    string fieldsValues = "";
                                    string fields = "";
                                    string values = "";
                                    IList<object> row = rows[i];

                                    bool hasNullFields = false;

                                    for (int j = 0; j < qtdCol; j++)
                                    {
                                        QueryFieldsMetadata field = fieldsMetadata[j];
                                        string name = field.name;
                                        object value = row[j];

                                        if ((field.userType?.Equals("I") ?? false) && value == null)
                                        {
                                            Console.WriteLine("Registro não inserido: " + string.Join(", ", row));
                                            hasNullFields = true; // Marca que há campos nulos
                                            break;
                                        }

                                        if (field.userType == "S" && value != null)
                                            value = $"'{value?.ToString().Trim()}'";
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
                                            string[] keydata = key.Split(',');
                                            if (keydata.Contains(name))
                                            {
                                                value = $"'{value?.ToString().Trim()}'";
                                                where += $"{name} = {value} AND ";
                                            }
                                        }
                                    }
                                    if (hasNullFields) // Verifica se há campos nulos
                                        continue; // continua apos acar o erro
                                    fieldsValues = fieldsValues.TrimEnd(',');
                                    fields = fields.TrimEnd(',');
                                    values = values.TrimEnd(',');
                                    if (where.Count() > 0)
                                    {
                                        where = where.TrimEnd("AND ".ToCharArray()); // Corrigido
                                        string query = $"SELECT {key} FROM {table} WHERE {where}";
                                        Console.WriteLine(query);
                                        string chave = con.ExecuteScalar<string>(query);
                                        var cSql = "";
                                        if (chave != null)
                                            cSql = $"UPDATE {table} SET {fieldsValues} WHERE {where}";
                                        else
                                            cSql = $"INSERT INTO {table} ({fields}) VALUES({values})";
                                        Console.WriteLine(cSql);
                                        sqlStatements.Add(cSql); // Adiciona a instrução SQL à li
                                    }
                                }

                                foreach (var sqlStatement in sqlStatements)
                                {
                                    int resultado = con.Execute(sqlStatement);
                                }

                                con.Close();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro: " + data?.statusMessage);
                    }
                }
            }


            catch (Exception e)
            {
                
            }

            return "Sucesso";
        }

        private static bool IsIntegerField(QueryFieldsMetadata field, object value)
        {
            if (value == null)
            {
                return IsNumericType(field);
            }
            return false;
        }


        private static bool IsNumericType(QueryFieldsMetadata field)
        {
            string fieldType = field.GetType().Name.ToLower();
            return fieldType.Contains("int") || fieldType.Contains("decimal") || fieldType.Contains("double");
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