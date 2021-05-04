using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.API{
    public interface IRepository{
        Task<Dictionary<int, string>> GetRepositories(string user);

        Task GetRepository(string userName, string repositoryName);

        string Name{ get; set; }
        string OpenIssues{ get; set; }
        string Permissions{ get; set; }
    }
}