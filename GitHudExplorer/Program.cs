using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using TinyBrowser;

namespace GitHudExplorer{
    class Program{
        static readonly HashSet<string> IgnoreKeyWords = new(){"components", "assets", "login", "logout", "shop"};
        const string Language = "en-us";

        static void Main(string[] args){
            Testing();
            //Run();
            while (true){
            }
        }

        static async void Testing(){
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Token", "SuperToken");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "_userAgent");
            var responseTxt = await httpClient.GetStringAsync("https://api.github.com/users/alex-rynkowski");
            Console.WriteLine(responseTxt);
        }

        static async void Run(){
            var userInput = "";
            while (true){
                var explorer = new WowExplorer();
                var sites = await explorer.AddLinksToDictionary(userInput);

                var dic = RemoveDuplicates(sites);
                foreach (var (key, value) in dic){
                    Console.WriteLine($"{key}  {value}");
                }

                userInput = sites[int.Parse(Console.ReadLine())];
            }
        }

        static Dictionary<int, string> RemoveDuplicates(Dictionary<int, string> siteDictionary){
            var sitesDictionary = new Dictionary<int, string>();
            var index = 0;

            foreach (var (key, value) in siteDictionary){
                if (sitesDictionary.Values.Contains(value)) continue;
                if (!IsCorrectLanguage(value) || ShouldIgnore(value)) continue;
                index++;
                sitesDictionary[index] = value;
            }

            return sitesDictionary;
        }

        static bool IsCorrectLanguage(string value){
            if (value.Contains('-')){
                var index = value.IndexOf('-');
                if (value[index - 3] == '/'){
                    return value.Contains(Language);
                }
            }

            return false;
        }

        static bool ShouldIgnore(string value){
            return IgnoreKeyWords.Any(value.Contains);
        }
    }
}