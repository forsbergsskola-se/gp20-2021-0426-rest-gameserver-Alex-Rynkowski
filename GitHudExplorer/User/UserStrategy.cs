using System;
using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class UserStrategy{
        public async Task User(){
            IGitHubApi gitHubApi = new GitHubApi();
            var user = await gitHubApi.GetUser("/user");
            var repositories = await user.Repository(new UserRepository()).GetRepositories(user.Login);

            foreach (var (key,value) in repositories){
                Custom.WriteLine($"{key}: {value}", ConsoleColor.White);
            }
        }
    }
}