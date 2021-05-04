using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.User{
    public interface IIssues{
        Task<List<string>> GetIssuesList(string userName, string repositoryName);
        string Title{ get; set; }
        string Body{ get; set; }
        string Login{ get; set; }
    }
}