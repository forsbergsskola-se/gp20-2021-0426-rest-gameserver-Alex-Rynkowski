using System.Text.Json.Serialization;

namespace GitHudExplorer.API{
    public interface IUser{
        IRepository Repository();

        string Name{ get; set; }
        string Company{ get; set; }
        string Location{ get; set; }
        string Login{ get; set; }

        string Description();
    }
}