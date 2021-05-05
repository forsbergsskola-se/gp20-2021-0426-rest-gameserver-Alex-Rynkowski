using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHudExplorer.Environment;
using GitHudExplorer.User;

namespace GitHudExplorer.Utilities{
    public static class Connection{
        static HttpClient httpClient;

        public static async Task<string> GetFromUrl(string url){
            httpClient = new HttpClient{BaseAddress = new Uri("https://api.github.com")};

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Token",
                    Token.PrivateAccessToken);
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

            httpClient.Timeout = TimeSpan.FromSeconds(2f);
            var response = await httpClient.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task CreateIssue(string url, string title, string body){
            var request = new HttpRequestMessage();
            var jsonOutput = JsonSerializer.Serialize(new Issue(title, body));
            request.RequestUri = new Uri("https://api.github.com" + url);
            request.Content = new StringContent(jsonOutput);
            await httpClient.PostAsync(request.RequestUri, request.Content);
            Custom.WriteLine("Issue was successfully created", ConsoleColor.DarkGreen);
        }
    }
}