using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.OtherUser{
    public class OtherRepository : IRepository{
        public async Task<Dictionary<int, string>> GetRepositories(string user){
            var repoDictionary = new Dictionary<int, string>();
            var response = await Connection.GetFromUrl($"/users/{user}/repos");

            var repositoryList = JsonSerializer.Deserialize<List<OtherRepository>>(response);

            if (repositoryList == null)
                throw new NoRepositoriesFoundException("No repositories found");

            for (var i = 0; i < repositoryList.Count; i++){
                repoDictionary[i] = repositoryList[i].Name;
            }

            return repoDictionary;
        }
        public async Task GetRepository(string url){
            var response = await Connection.GetFromUrl(url);
            Console.WriteLine(response);
        }

        [JsonPropertyName("name")] public string Name{ get; set; }
    }
}