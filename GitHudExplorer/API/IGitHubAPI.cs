using System.Threading.Tasks;

namespace GitHudExplorer.API{
    public interface IGitHubApi{
        Task<IUser> GetUser(string userName);
    }
}