using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.User{
    public interface IAllIssues{
        Task<List<string>> GetIssuesList(string userName, string repositoryName);

        IIssue Issue();
    }
}