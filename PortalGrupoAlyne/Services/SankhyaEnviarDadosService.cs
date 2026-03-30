using System.Text.Json;
using Dapper;
using MySqlConnector;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaEnviarDadosService
    {
        static IConfiguration? _configuration;

        public static async Task<Object> processarPedido(IConfiguration configuration, PedidoVendaRequest pedido)
        {
            Object resultado = "";
            try
            {
                LoginResponse? result = (LoginResponse?)await SankhyaService.login(configuration);
                if (result != null && result.status == "1") //SankhyaService.getJsessionid() == null) 
                {
                    _configuration = configuration;
                    // ------------------------ Enviar Pedido ---------------------------
                    resultado = await SankhyaService.EnviarPedidoItensPrimeiro(configuration, pedido);

                    // ------------------------ Logout ----------------------------------
                    await SankhyaService.logout(configuration);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                return "error: " + e.Message;
            }
            return resultado;
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

        public static async Task<String> AtualizarTabela(string sql, string table, string key)
        {
            try
            {
                Console.WriteLine(sql);
                QueryResponse data = await SankhyaService.executeQuery(_configuration, sql);
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
