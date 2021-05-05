using System.Threading.Tasks;

namespace GitHudExplorer.User{
    public interface IIssue{
        Task CreateIssue(string userName, string repositoryName);
        string Title{ get; set; }
        string Body{ get; set; }
        string Login{ get; set; }
    }
}