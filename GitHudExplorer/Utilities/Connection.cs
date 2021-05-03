using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHudExplorer.Utilities{
    public static class Connection{
        public static async Task<string> GetFromUrl(string url){
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Token", "dsaaa");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "_userAgent");

            httpClient.Timeout = TimeSpan.FromSeconds(2f);
            return await httpClient.GetStringAsync(url);
        }
    }
}