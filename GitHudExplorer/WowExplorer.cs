using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinyBrowser{
    public class WowExplorer{
        public async void AddLinksToDictionary(){
            var tmp = await ReceiveInfoFromSite("https://worldofwarcraft.com/en-us/");
            var txtStartIndex = 0;
            var siteIndex = 0;
            while (true){
                var foundLink = FindTextBetween(tmp, "<a href=\"", "\"", ref txtStartIndex);

                if (foundLink == "") break;

                if (foundLink.StartsWith('/')){
                    foundLink = foundLink.Substring(1);
                }

                Console.WriteLine($"{siteIndex} {foundLink}");
                siteIndex++;
            }

            static async Task<string> ReceiveInfoFromSite(string site){
                var httpClient = new HttpClient();

                var response = await httpClient.PostAsync(site,
                    new FormUrlEncodedContent(new Dictionary<string, string>()));
                var contents = await response.Content.ReadAsStringAsync();

                return contents;
            }


            static string FindTextBetween(string textToSearch, string startText, string endText,
                ref int startAtIndex){
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
}