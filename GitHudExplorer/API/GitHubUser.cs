using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitHudExplorer.API{
    public class GitHubUser : IUser{
        readonly Dictionary<string, string> userInfo;

        public GitHubUser(){
            this.userInfo = userInfo;
        }

        public IRepository Repository(){
            var repo = new Repository();
            return repo;
        }

        [JsonPropertyName("name")]
        public string Name{ get; set; }
        [JsonPropertyName("company")]
        public string Company{ get; set; }
        [JsonPropertyName("location")]
        public string Location{ get; set; }
        public string Description() =>
            $"{this.Name} from {this.Location} is working for {this.Company}";
    }
}