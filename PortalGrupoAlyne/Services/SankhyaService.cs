using System.Net.Http.Headers;
using System.Text;
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
            request.Content = new StringContent(body, Encoding.Latin1);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

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
            request.Content = new StringContent(body, Encoding.Latin1);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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
            request.Content = new StringContent(body, Encoding.Latin1);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            QueryResponse? result = await response.Content.ReadFromJsonAsync<QueryResponse>();

            return result;
        }

        /*
        public static async Task<QueryResponse> execQuery(string sql)
        {
            var url      = $"{EndPoint}mge/service.sbr?serviceName=DbExplorerSP.executeQuery&outputType=json";
            string body  = @"{""serviceName"" : ""DbExplorerSP.executeQuery"", 
                                ""requestBody"": {
                                    ""sql"": ""query""
                                }
                            }";
            body = body.Replace("query", sql);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body, Encoding.Latin1);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jsessionid);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            QueryResponse? result = await response.Content.ReadFromJsonAsync<QueryResponse>();
            return result;
        }        
        */

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
    }
}
