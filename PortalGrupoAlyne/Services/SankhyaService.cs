using System.Net.Http.Headers;
using System.Text;
using PortalGrupoAlyne.Model.Dtos.Sankhya;

namespace PortalGrupoAlyne.Services
{
    public class SankhyaService //: ISankhyaService
    {
        
        const string EndPoint = "http://10.0.0.254:8280/";
        //const string EndPoint = "http://10.0.0.253:8180/";
        static HttpClient client = new HttpClient();
        static string jsessionid = null;
        //public SankhyaService() { }

        public static async Task<Object> login()
        {
            var url     = $"{EndPoint}mge/service.sbr?serviceName=MobileLoginSP.login&outputType=json";
            var body    = @"{""serviceName"": ""MobileLoginSP.login"", 
                                ""requestBody"":{
                                    ""NOMUSU"":{""$"":""ADMIN""},
                                    ""INTERNO"":{""$"":""SYNC550V""},
                                    ""KEEPCONNECTED"":{""$"":""S""} 
                                }
                            }";

            //Console.WriteLine("body");
            //Console.WriteLine(body);
            
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

        public static async Task<Object> logout()
        {
            var url     = $"{EndPoint}mge/service.sbr?serviceName=MobileLoginSP.logout&outputType=json";
            var body    = @"{""serviceName"" : ""MobileLoginSP.logout"", 
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

        public static async Task<QueryResponse> executeQuery(string sql)
        {
            var url      = $"{EndPoint}mge/service.sbr?serviceName=DbExplorerSP.executeQuery&outputType=json";
            string body  = @"{""serviceName"" : ""DbExplorerSP.executeQuery"", 
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

        public static string getJsessionid()
        {
            return jsessionid;
        }

        /*
        public static string ConverteObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }
        public static T ConverteJSonParaObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        */

    }
}
