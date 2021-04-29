using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinyBrowser{
    public class WowExplorer{
        const string wowMainSite = "https://github.com/";

        public async Task<Dictionary<int, string>> AddLinksToDictionary(string site){
            var sitesDictionary = new Dictionary<int, string>();
            var siteContent = await ReceiveInfoFromSite(site);
            var txtStartIndex = 0;
            var siteIndex = 0;
            Console.WriteLine(siteContent);
            while (true){
                var foundLink = FindTextBetween(siteContent, "href=\"", "\"", ref txtStartIndex);

                if (foundLink == "") break;

                // if (foundLink.StartsWith('/')){
                //     foundLink = foundLink.Substring(1);
                // }

                sitesDictionary[siteIndex] = foundLink;
                siteIndex++;
            }

            return sitesDictionary;
        }

        static async Task<string> ReceiveInfoFromSite(string site){
            var httpClient = new HttpClient();

            if (string.IsNullOrEmpty(site))
                site = wowMainSite;
            var response = await httpClient.PostAsync(site,
                new FormUrlEncodedContent(new Dictionary<string, string>()));
            var contents = await response.Content.ReadAsStringAsync();
            Console.WriteLine(contents);
            return contents;
        }


        static string FindTextBetween(string textToSearch, string startText, string endText, ref int startAtIndex){
            var startIndex = textToSearch.IndexOf(startText, startAtIndex, StringComparison.Ordinal) +
                             startText.Length;
            var endIndex = textToSearch.IndexOf(endText, startIndex, StringComparison.Ordinal);

            var findLastIndex = textToSearch.LastIndexOf(startText, endIndex, StringComparison.Ordinal);
            if (startAtIndex > findLastIndex)
                return "";

            startAtIndex = endIndex + 1;
            var newText = textToSearch.Remove(endIndex).Substring(startIndex);
            return newText;
        }
    }
}