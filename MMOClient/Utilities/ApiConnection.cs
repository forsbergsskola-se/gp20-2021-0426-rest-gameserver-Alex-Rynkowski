﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.Api;
using Newtonsoft.Json;

namespace Client.Utilities{
    public static class ApiConnection{
        public static async Task<T> GetResponse<T>(string url){
            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            var client = new HttpClient(clientHandler){BaseAddress = new Uri("https://localhost:44317/players")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response =
                await client.GetAsync(client.BaseAddress + url);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task SendRequest(string url){
            var clientHandler = new HttpClientHandler{
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage{
                RequestUri = new Uri("https://localhost:44317/players/Create/SuperNoob")
            };

            var player = new Player();
            request.Content = new StringContent(JsonConvert.SerializeObject(player));
            var tmp = await client.PostAsync(request.RequestUri, request.Content);
        }
    }
}