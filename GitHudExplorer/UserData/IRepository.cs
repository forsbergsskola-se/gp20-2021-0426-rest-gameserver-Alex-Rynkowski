using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.UserData{
    public interface IRepository{
        Task<List<string>> GetRepositoryList(string url);
        
        
        string Name{ get; }
        string Description{ get; }
    }
}