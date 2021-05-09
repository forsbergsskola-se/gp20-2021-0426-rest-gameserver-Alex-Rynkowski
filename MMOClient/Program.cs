using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client{
    class Program{
        static async Task Main(string[] args){
            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            
            var client = new HttpClient(clientHandler){BaseAddress = new Uri("https://localhost:44317/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response =
                await client.GetAsync(client.BaseAddress + "players/Get/92e7ef3e-45c5-44f2-a3b2-be28d76ceb6f");

            Console.WriteLine(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
            Console.WriteLine("Hello World!");
        }
    }
}