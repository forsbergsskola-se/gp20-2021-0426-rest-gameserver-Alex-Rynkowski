using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.UserData{
    public interface IRepository{
        Task<Dictionary<int, string>> GetRepositoryList(string url);
        
        Task GetRepository(string url);
        
        string Name{ get; }
        string Description{ get; }
    }
}