using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class Issue : IIssue{
        [JsonPropertyName("title")] public string Title{ get; set; }
        [JsonPropertyName("body")] public string Body{ get; set; }
        [JsonPropertyName("login")] public string Login{ get; set; }

        public Issue(string title = "Default Title", string body = "Default Body"){
            this.Title = title;
            this.Body = body;
        }

        public async Task CreateIssue(string userName, string repositoryName){
            Custom.WriteLine("Issue title:", ConsoleColor.Yellow);
            var title = Custom.ReadLine(ConsoleColor.Green);
            Custom.WriteLine("Describe the issue:", ConsoleColor.Yellow);
            var body = Custom.ReadLine(ConsoleColor.Green);

            await Connection.CreateIssue($"/repos/{userName}/{repositoryName}/issues", title, body);
        }
    }
}