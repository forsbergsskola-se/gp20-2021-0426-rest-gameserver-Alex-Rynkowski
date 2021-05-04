using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class UserRepository : IRepository{
        public async Task<Dictionary<int, string>> GetRepositories(string user){
            var newDic = new Dictionary<int, string>();
            var response = await Connection.GetFromUrl("/user/repos");

            var repositoriesList = JsonSerializer.Deserialize<List<Repositories>>(response);

            for (var i = 0; i < repositoriesList.Count; i++){
                newDic[i] = repositoriesList[i].Name;
            }

            return newDic;
        }

        public async Task GetRepository(string userName, string repositoryName){
            Console.WriteLine($"{userName}   {repositoryName}");
            var response = await Connection.GetFromUrl($"/repos/{userName}/{repositoryName}");

            var responseDe = JsonSerializer.Deserialize<UserRepository>(response);

            Console.WriteLine(responseDe.Name);
            Console.WriteLine(responseDe.OpenIssues);
            Console.WriteLine(responseDe.Permissions);
        }

        [JsonPropertyName("name")] public string Name{ get; set; }
        [JsonPropertyName("open")] public string OpenIssues{ get; set; }
        public string Permissions{ get; set; }
    }

    public class Repositories : IRepositories{
        [JsonPropertyName("name")] public string Name{ get; set; }
    }
}