using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;
using Dapper;
using MySqlConnector;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaService //: ISankhyaService
    {

        //const string EndPoint = "http://10.0.0.254:8280/";
        //const string EndPoint = "http://10.0.0.253:8180/";
        static HttpClient client = new HttpClient();
        static string jsessionid = null;
        //public SankhyaService() { }

        public static async Task<Object> login(IConfiguration configuration)
        {
            Configuracao configuracao = obterConfiguracoes(configuration);
            string url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=MobileLoginSP.login&outputType=json";
            string body = @"{""serviceName"": ""MobileLoginSP.login"", 
                                ""requestBody"":{
                                    ""NOMUSU"":{""$"":""$SankhyaUsuario""},
                                    ""INTERNO"":{""$"":""$SankhyaSenha""},
                                    ""KEEPCONNECTED"":{""$"":""S""} 
                                }
                            }";
            body = body.Replace("$SankhyaUsuario", configuracao.SankhyaUsuario);
            body = body.Replace("$SankhyaSenha", configuracao.SankhyaSenha);
            //Console.WriteLine("url" + url);
            //Console.WriteLine("body" + body);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result != null && result.status == "1") jsessionid = result.responseBody.jsessionid.ATRIBUTO;

            return result;
        }

        public static async Task<Object> logout(IConfiguration configuration)
        {
            Configuracao configuracao = obterConfiguracoes(configuration);
            string url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=MobileLoginSP.logout&outputType=json";
            string body = @"{""serviceName"" : ""MobileLoginSP.logout"", 
                ""status"":""1"",
                ""pendingPrinting"":""false"",
            }";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            LogoutResponse? result = await response.Content.ReadFromJsonAsync<LogoutResponse>();

            return result;
        }

        public static async Task<QueryResponse> executeQuery(IConfiguration configuration, string sql)
        {
            Configuracao configuracao = obterConfiguracoes(configuration);
            string url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=DbExplorerSP.executeQuery&outputType=json";
            string body = @"{""serviceName"" : ""DbExplorerSP.executeQuery"", 
                                ""requestBody"": {
                                    ""sql"": ""query""
                                }
                            }";
            body = body.Replace("query", sql);
            //Console.WriteLine(body);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            QueryResponse? result = await response.Content.ReadFromJsonAsync<QueryResponse>();

            return result;
        }

        public static string getJsessionid()
        {
            return jsessionid;
        }

        public static Configuracao obterConfiguracoes(IConfiguration configuration)
        {
            string MySqlCon = configuration.GetConnectionString("DefaultConnection");
            using var con = new MySqlConnection(MySqlCon);
            con.Open();
            Configuracao configuracao = con.QueryFirst<Configuracao>("SELECT * FROM Configuracao WHERE id=@id", new { id = 1 });
            con.Close();
            return configuracao;
        }

        public static async Task<Object> EnviarPedido(IConfiguration configuration, PedidoVendaRequest pedido)
        {
            var result = "Erro ao utilizar a API Sankhya";
            Configuracao configuracao = obterConfiguracoes(configuration);
            StringBuilder sb = new StringBuilder();
            string url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=CRUDServiceProvider.saveRecord&Application=DynaformLauncher&mgeSession={jsessionid}&resourceID=br.com.sankhya.menu.adicional.AD_Z38";
            string body = "";
            string sValor;

            sValor = pedido.CabecalhoPedidoVenda.Valor.ToString();
            sValor = sValor.Replace(",", ".");

            sb.AppendLine(@"<serviceRequest serviceName=""CRUDServiceProvider.saveRecord"">");
            sb.AppendLine(" <requestBody>");
            sb.AppendLine(@"    <dataSet rootEntity=""AD_Z38"" includePresentationFields=""S"" datasetid=""1659004301295_1"">");
            sb.AppendLine(@"        <entity path=""""><fieldset list=""*""/></entity>");
            sb.AppendLine(@"        <entity path=""Parceiro""><field name=""NOMEPARC""/></entity>");
            sb.AppendLine("         <dataRow>");
            sb.AppendLine("             <localFields>");
            sb.AppendLine($"                 <PALMPV><![CDATA[{pedido.CabecalhoPedidoVenda.PalmPV}]]></PALMPV>");
            sb.AppendLine($"                 <EMP>{pedido.CabecalhoPedidoVenda.Filial}</EMP>");
            sb.AppendLine($"                 <TIPPED>{pedido.CabecalhoPedidoVenda.TipPed}</TIPPED>");
            sb.AppendLine($"                 <VEND>{pedido.CabecalhoPedidoVenda.VendedorId.ToString()}</VEND>");
            sb.AppendLine($"                 <CLIENT>{pedido.CabecalhoPedidoVenda.ParceiroId.ToString()}</CLIENT>");
            sb.AppendLine($"                 <COND>{pedido.CabecalhoPedidoVenda.TipoNegociacaoId.ToString()}</COND>");
            sb.AppendLine($"                <DATA>{pedido.CabecalhoPedidoVenda.Data.ToString("dd/MM/yyyy")}</DATA>");
            sb.AppendLine($"                 <VALOR>{sValor}</VALOR>");
            sb.AppendLine($"                 <DTENTR>{pedido.CabecalhoPedidoVenda.DataEntrega.ToString("dd/MM/yyyy")}</DTENTR>");
            sb.AppendLine($"                 <OBS><![CDATA[{pedido.CabecalhoPedidoVenda.Observacao}]]></OBS>");
            sb.AppendLine("             </localFields>");
            sb.AppendLine("         </dataRow>");
            sb.AppendLine("     </dataSet>");
            sb.AppendLine("     <clientEventList/>");
            sb.AppendLine(" </requestBody>");
            sb.AppendLine("</serviceRequest>");

            body = sb.ToString();

            // Console.WriteLine(body);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);


            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var xmlSerializer = new XmlSerializer(typeof(PedidoVendaResponse));
            StringReader textReader = new StringReader(responseBody);
            var result2 = xmlSerializer.Deserialize(textReader);

            string status = responseBody.Substring(responseBody.IndexOf("status"), 9);
            status = status.Substring(8, 1);

            // *************************
            // Se obtiver sucesso ao enviar ao cabe�alho, envia tamb�m os itens
            // *************************
            if (status == "1")
            {
                //StringBuilder sb = new StringBuilder();
                //result = "Erro ao utilizar a API Sankhya";
               // result = responseBody;
                url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=CRUDServiceProvider.saveRecord&Application=DynaformLauncher&mgeSession={jsessionid}&resourceID=br.com.sankhya.menu.adicional.AD_Z39";
                sb.Clear();
                sb.AppendLine(@"<serviceRequest serviceName=""CRUDServiceProvider.saveRecord"">");
                sb.AppendLine(" <requestBody>");
                sb.AppendLine(@"    <dataSet rootEntity=""AD_Z39"" includePresentationFields=""S"" datasetid=""1667584438688_4"">");
                sb.AppendLine(@"        <entity path=""""><fieldset list=""*""/></entity>");
                sb.AppendLine(@"        <entity path=""Produto""><field name=""DESCRPROD""/></entity>");

                IList<Model.Dtos.Sankhya.ItemPedidoVenda> itens = pedido.ItemPedidoVenda;

                decimal total = 0;
                string sQuant, sValUnit;
                foreach (var item in itens)
                {
                    sQuant = item.Quant.ToString();
                    sQuant = sQuant.Replace(",", ".");
                    sValUnit = item.ValUnit.ToString();
                    sValUnit = sValUnit.Replace(",", ".");

                    sb.AppendLine("         <dataRow>");
                    sb.AppendLine("             <localFields>");
                    sb.AppendLine($"                <VEND>{item.VendedorId}</VEND>");
                    sb.AppendLine($"                <CODPRO>{item.ProdutoId}</CODPRO>");
                    sb.AppendLine($"                <QTDE>{sQuant}</QTDE>");
                    sb.AppendLine($"                <PUNIT>{sValUnit}</PUNIT>");
                    sb.AppendLine("             </localFields>");
                    sb.AppendLine("             <foreingKey>");
                    sb.AppendLine($"                <PALMPV><![CDATA[{pedido.CabecalhoPedidoVenda.PalmPV}]]></PALMPV>");
                    sb.AppendLine("             </foreingKey>");
                    sb.AppendLine("         </dataRow>");

                    total += item.Quant * item.ValUnit;
                }

                sb.AppendLine("     </dataSet>");
                sb.AppendLine("     <clientEventList/>");
                sb.AppendLine("  </requestBody>");
                sb.AppendLine("</serviceRequest>");

                body = sb.ToString();
                Console.WriteLine(body);

                request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();

                xmlSerializer = new XmlSerializer(typeof(PedidoVendaResponse));
                textReader = new StringReader(responseBody);
                result2 = xmlSerializer.Deserialize(textReader);

                status = responseBody.Substring(responseBody.IndexOf("status"), 9);
                status = status.Substring(8, 1);
                if (status == "1")
                {
                    result = "Sucesso";
                }
                else
                {

                    PedidoVendaResponse? pedRes = result2 as PedidoVendaResponse;
                    string txtenc = pedRes.statusMessage.ToString();

                    byte[] data = Convert.FromBase64String(txtenc);
                    // string decotxt = Encoding.UTF8.GetString(data);
                    string decotxt = Encoding.GetEncoding("ISO-8859-1").GetString(data);
                    result = decotxt;

                }
            }
            else
            {
                PedidoVendaResponse? pedRes = result2 as PedidoVendaResponse;

                string txtenc = pedRes.statusMessage.ToString();

                byte[] data = Convert.FromBase64String(txtenc);
                // string decotxt = Encoding.UTF8.GetString(data);
                string decotxt = Encoding.GetEncoding("ISO-8859-1").GetString(data);

                result = decotxt;
            }


            return result;
        }

        public static async Task<Object> EnviarPedidoItensPrimeiro(IConfiguration configuration, PedidoVendaRequest pedido)
        {
            var result = "Erro ao utilizar a API Sankhya";
            Configuracao configuracao = obterConfiguracoes(configuration);
            StringBuilder sb = new StringBuilder();
            string url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=CRUDServiceProvider.saveRecord&Application=DynaformLauncher&mgeSession={jsessionid}&resourceID=br.com.sankhya.menu.adicional.AD_Z39";
            string body;
            HttpRequestMessage request = null!;
            HttpResponseMessage response = null!;
            string responseBody = "";
            string status = "";

            IList<Model.Dtos.Sankhya.ItemPedidoVenda> itens = pedido?.ItemPedidoVenda ?? new List<Model.Dtos.Sankhya.ItemPedidoVenda>();
            string palmPv = pedido?.CabecalhoPedidoVenda?.PalmPV ?? "";
            int expectedCount = itens.Count;

            string lastItemError = "";
            bool itensOk = false;
            for (int tentativa = 0; tentativa < 3; tentativa++)
            {
                int existentes = await ObterQuantidadeItensAdZ39(configuration, palmPv);
                if (existentes > 0)
                {
                    await ApagarItensAdZ39(configuration, palmPv);
                }

                sb.Clear();
                sb.AppendLine(@"<serviceRequest serviceName=""CRUDServiceProvider.saveRecord"">");
                sb.AppendLine(" <requestBody>");
                sb.AppendLine(@"    <dataSet rootEntity=""AD_Z39"" includePresentationFields=""S"" datasetid=""1667584438688_4"">");
                sb.AppendLine(@"        <entity path=""""><fieldset list=""*""/></entity>");
                sb.AppendLine(@"        <entity path=""Produto""><field name=""DESCRPROD""/></entity>");

                string sQuant, sValUnit;
                foreach (var item in itens)
                {
                    sQuant = item.Quant.ToString();
                    sQuant = sQuant.Replace(",", ".");
                    sValUnit = item.ValUnit.ToString();
                    sValUnit = sValUnit.Replace(",", ".");

                    sb.AppendLine("         <dataRow>");
                    sb.AppendLine("             <localFields>");
                    sb.AppendLine($"                <VEND>{item.VendedorId}</VEND>");
                    sb.AppendLine($"                <CODPRO>{item.ProdutoId}</CODPRO>");
                    sb.AppendLine($"                <QTDE>{sQuant}</QTDE>");
                    sb.AppendLine($"                <PUNIT>{sValUnit}</PUNIT>");
                    sb.AppendLine("             </localFields>");
                    sb.AppendLine("             <foreingKey>");
                    sb.AppendLine($"                <PALMPV><![CDATA[{palmPv}]]></PALMPV>");
                    sb.AppendLine("             </foreingKey>");
                    sb.AppendLine("         </dataRow>");
                }

                sb.AppendLine("     </dataSet>");
                sb.AppendLine("     <clientEventList/>");
                sb.AppendLine("  </requestBody>");
                sb.AppendLine("</serviceRequest>");

                body = sb.ToString();

                request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                status = ExtrairStatus(responseBody);
                if (status != "1")
                {
                    lastItemError = DecodeStatusMessage(responseBody);
                    if (tentativa < 2)
                    {
                        await ApagarItensAdZ39(configuration, palmPv);
                        continue;
                    }
                    return lastItemError;
                }

                int qtdAposInserir = await ObterQuantidadeItensAdZ39(configuration, palmPv);
                if (qtdAposInserir == expectedCount)
                {
                    itensOk = true;
                    break;
                }

                lastItemError = $"Quantidade de itens no Sankhya ({qtdAposInserir}) diferente do esperado ({expectedCount}).";
                await ApagarItensAdZ39(configuration, palmPv);
            }

            if (!itensOk)
            {
                return string.IsNullOrWhiteSpace(lastItemError) ? "Falha ao enviar itens ao Sankhya." : lastItemError;
            }

            url = $"{configuracao.SankhyaServidor}mge/service.sbr?serviceName=CRUDServiceProvider.saveRecord&Application=DynaformLauncher&mgeSession={jsessionid}&resourceID=br.com.sankhya.menu.adicional.AD_Z38";
            sb.Clear();
            string sValor = pedido.CabecalhoPedidoVenda.Valor.ToString();
            sValor = sValor.Replace(",", ".");

            sb.AppendLine(@"<serviceRequest serviceName=""CRUDServiceProvider.saveRecord"">");
            sb.AppendLine(" <requestBody>");
            sb.AppendLine(@"    <dataSet rootEntity=""AD_Z38"" includePresentationFields=""S"" datasetid=""1659004301295_1"">");
            sb.AppendLine(@"        <entity path=""""><fieldset list=""*""/></entity>");
            sb.AppendLine(@"        <entity path=""Parceiro""><field name=""NOMEPARC""/></entity>");
            sb.AppendLine("         <dataRow>");
            sb.AppendLine("             <localFields>");
            sb.AppendLine($"                 <PALMPV><![CDATA[{pedido.CabecalhoPedidoVenda.PalmPV}]]></PALMPV>");
            sb.AppendLine($"                 <EMP>{pedido.CabecalhoPedidoVenda.Filial}</EMP>");
            sb.AppendLine($"                 <TIPPED>{pedido.CabecalhoPedidoVenda.TipPed}</TIPPED>");
            sb.AppendLine($"                 <VEND>{pedido.CabecalhoPedidoVenda.VendedorId.ToString()}</VEND>");
            sb.AppendLine($"                 <CLIENT>{pedido.CabecalhoPedidoVenda.ParceiroId.ToString()}</CLIENT>");
            sb.AppendLine($"                 <COND>{pedido.CabecalhoPedidoVenda.TipoNegociacaoId.ToString()}</COND>");
            sb.AppendLine($"                <DATA>{pedido.CabecalhoPedidoVenda.Data.ToString("dd/MM/yyyy")}</DATA>");
            sb.AppendLine($"                 <VALOR>{sValor}</VALOR>");
            sb.AppendLine($"                 <DTENTR>{pedido.CabecalhoPedidoVenda.DataEntrega.ToString("dd/MM/yyyy")}</DTENTR>");
            sb.AppendLine($"                 <OBS><![CDATA[{pedido.CabecalhoPedidoVenda.Observacao}]]></OBS>");
            sb.AppendLine("             </localFields>");
            sb.AppendLine("         </dataRow>");
            sb.AppendLine("     </dataSet>");
            sb.AppendLine("     <clientEventList/>");
            sb.AppendLine(" </requestBody>");
            sb.AppendLine("</serviceRequest>");

            body = sb.ToString();

            request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

            response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            status = ExtrairStatus(responseBody);

            if (status == "1")
            {
                result = "Sucesso";
            }
            else
            {
                result = DecodeStatusMessage(responseBody);
            }

            return result;
        }

        private static async Task<int> ObterQuantidadeItensAdZ39(IConfiguration configuration, string palmPv)
        {
            try
            {
                string sql = $"SELECT COUNT(1) AS QTD FROM AD_Z39 WHERE PALMPV = {ToSqlString(palmPv)}";
                QueryResponse resp = await executeQuery(configuration, sql);
                if (resp == null || resp.status != "1") return 0;
                if (resp.responseBody.rows == null || resp.responseBody.rows.Count == 0) return 0;
                if (resp.responseBody.rows[0] == null || resp.responseBody.rows[0].Count == 0) return 0;
                object valor = resp.responseBody.rows[0][0];
                if (valor == null) return 0;
                if (int.TryParse(valor.ToString(), out int qtd)) return qtd;
                if (decimal.TryParse(valor.ToString(), out decimal qtdDec)) return (int)qtdDec;
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private static async Task<bool> ApagarItensAdZ39(IConfiguration configuration, string palmPv)
        {
            try
            {
                string sql = $"DELETE FROM AD_Z39 WHERE PALMPV = {ToSqlString(palmPv)}";
                QueryResponse resp = await executeQuery(configuration, sql);
                return resp != null && resp.status == "1";
            }
            catch
            {
                return false;
            }
        }

        private static string ToSqlString(string value)
        {
            string safe = (value ?? "").Replace("'", "''");
            return $"'{safe}'";
        }

        private static string ExtrairStatus(string responseBody)
        {
            string status = responseBody.Substring(responseBody.IndexOf("status"), 9);
            return status.Substring(8, 1);
        }

        private static string DecodeStatusMessage(string responseBody)
        {
            var xmlSerializer = new XmlSerializer(typeof(PedidoVendaResponse));
            StringReader textReader = new StringReader(responseBody);
            var result2 = xmlSerializer.Deserialize(textReader);
            PedidoVendaResponse? pedRes = result2 as PedidoVendaResponse;
            string txtenc = pedRes?.statusMessage?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(txtenc))
            {
                return responseBody;
            }

            byte[] data = Convert.FromBase64String(txtenc);
            return Encoding.GetEncoding("ISO-8859-1").GetString(data);
        }

    }
}
