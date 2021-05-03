using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHudExplorer.Environment;

namespace GitHudExplorer.Utilities{
    public static class Connection{
        public static async Task<string> GetFromUrl(string url){
            var httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Token",
                    Token.PrivateAccessToken);
            
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

            httpClient.Timeout = TimeSpan.FromSeconds(2f);
            var tmp = await httpClient.GetAsync(url);

            //Console.WriteLine("Content: " + await tmp.Content.ReadAsStringAsync());
            return await tmp.Content.ReadAsStringAsync();
            //return await httpClient.GetStringAsync(url);
        }
    }
}