using System.Text.Json.Serialization;

namespace GitHudExplorer.API{
    public interface IUser{
        IRepository Repository();
        
        [JsonPropertyName("name")]
        string Name{ get; set; }
        
        [JsonPropertyName("company")]
        string Company{ get; set; }
        
        [JsonPropertyName("location")]
        string Location{ get; set; }

        string Description();
    }
}