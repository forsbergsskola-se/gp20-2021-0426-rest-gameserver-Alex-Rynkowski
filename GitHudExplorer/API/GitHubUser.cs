using System.Collections.Generic;
using System.Text.Json.Serialization;
using GitHudExplorer.OtherUser;

namespace GitHudExplorer.API{
    public class GitHubUser : IUser{
        public IRepository Repository(IRepository repository) {
            return repository;
        }

        [JsonPropertyName("name")] public string Name{ get; set; }
        [JsonPropertyName("company")] public string Company{ get; set; }
        [JsonPropertyName("location")] public string Location{ get; set; }
        [JsonPropertyName("login")] public string Login{ get; set; }

        public string Description() =>
            $"{this.Name} from {this.Location} is working for {this.Company}";
    }
}