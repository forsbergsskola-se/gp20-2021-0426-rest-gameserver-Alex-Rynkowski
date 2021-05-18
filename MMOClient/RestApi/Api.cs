using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.RestApi{
    public static class Api{
        public static async Task<T> GetResponse<T>(string url){
            var adjustedUrl = url;
            if (url[0] == '/')
                adjustedUrl = url[1..];

            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            var client = new HttpClient(clientHandler){BaseAddress = new Uri($"https://localhost:44317/api/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(client.BaseAddress + adjustedUrl);
            Console.WriteLine(client.BaseAddress + adjustedUrl);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<T> PostRequest<T>(string url, string jsonValue){
            var adjustedUrl = url;
            if (url[0] == '/')
                adjustedUrl = url[1..];

            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage{
                RequestUri = new Uri($"https://localhost:44317/api/{adjustedUrl}")
            };

            Console.WriteLine(request.RequestUri);
            request.Content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var httpResponseMessage = await client.PostAsync(request.RequestUri, request.Content);
            var link = client.BaseAddress + url;
            Console.WriteLine(link);
            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public static async Task<T> PutRequest<T>(string url, string jsonValue){
            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage{
                RequestUri = new Uri($"https://localhost:44317/api/{url}")
            };
            
            Console.WriteLine(request.RequestUri);
            request.Content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var httpResponseMessage = await client.PutAsync(request.RequestUri, request.Content);
            var link = client.BaseAddress + url;
            Console.WriteLine(link);
            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public static async Task<T> DeleteRequest<T>(string url){
            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            var client = new HttpClient(clientHandler){BaseAddress = new Uri($"https://localhost:44317/api/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Delete, client.BaseAddress + url);
            Console.WriteLine(client.BaseAddress + url);
            var tmp = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<T>(await tmp.Content.ReadAsStringAsync());
        }
    }
}