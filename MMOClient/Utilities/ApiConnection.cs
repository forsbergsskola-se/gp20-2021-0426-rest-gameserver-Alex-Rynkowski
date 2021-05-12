using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.Api;
using Newtonsoft.Json;

namespace Client.Utilities{
    public static class ApiConnection{
        public static async Task<T> GetResponse<T>(string url){
            var adjustedUrl = url;
            if (url[0] == '/')
                adjustedUrl = url[1..];

            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            var client = new HttpClient(clientHandler){BaseAddress = new Uri("https://localhost:44317/api/players/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(client.BaseAddress + adjustedUrl);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<T> SendRequest<T>(string url){
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
                RequestUri = new Uri($"https://localhost:44317/api/players/{adjustedUrl}")
            };

            Console.WriteLine(request.RequestUri);
            var player = new Player();
            request.Content = new StringContent(JsonConvert.SerializeObject(player));
            var tmp = await client.PostAsync(request.RequestUri, request.Content);
            return JsonConvert.DeserializeObject<T>(await tmp.Content.ReadAsStringAsync());
        }
    }
}