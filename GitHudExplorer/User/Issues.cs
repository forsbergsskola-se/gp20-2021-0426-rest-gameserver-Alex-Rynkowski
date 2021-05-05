using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class Issues : IIssues{
        public async Task<List<string>> GetIssuesList(string userName, string repositoryName){
            var response = await Connection.GetFromUrl($"/repos/{userName}/{repositoryName}/issues");

            var responseList = JsonSerializer.Deserialize<List<Issues>>(response);

            foreach (var issue in responseList){
                Console.WriteLine(issue.Title);
                Console.WriteLine(issue.Body);

                await Connection.CreateIssue($"/repos/{userName}/{repositoryName}/issues", "Tesssst",
                    "lol, wtf is this");
            }


            return default;
        }

        [JsonPropertyName("title")] public string Title{ get; set; }
        [JsonPropertyName("body")] public string Body{ get; set; }
        [JsonPropertyName("login")] public string Login{ get; set; }
    }
}