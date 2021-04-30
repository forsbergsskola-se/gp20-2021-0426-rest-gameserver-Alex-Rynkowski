using System.Threading.Tasks;

namespace GitHudExplorer.UserData{
    public interface IGitHubApi{
        Task<IUser> GetUser(string userName);
    }
}