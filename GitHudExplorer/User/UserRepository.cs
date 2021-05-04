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

            var repositoriesList = JsonSerializer.Deserialize<List<UserRepository>>(response);

            for (var i = 0; i < repositoriesList.Count; i++){
                newDic[i] = repositoriesList[i].Name;
            }
            return newDic;
        }

        public async Task GetRepository(string url){
            //throw new System.NotImplementedException();
        }

        [JsonPropertyName("name")] public string Name{ get; set; }
    }
}