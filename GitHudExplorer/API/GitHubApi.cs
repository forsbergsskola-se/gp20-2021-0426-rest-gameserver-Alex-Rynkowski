using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.API{
    class GitHubApi : IGitHubApi{
        IUser githubUser;
        public GitHubApi(){
            this.githubUser = new GitHubUser();
        }


        async Task<bool> UserExists(string url){
            string response;
            try{
                response = await Connection.GetFromUrl(url);
            }
            catch (Exception ex){
                switch (ex){
                    case InvalidOperationException:
                        Custom.WriteLine("Invalid address", ConsoleColor.Red);
                        break;
                    case HttpRequestException:
                        Custom.WriteLine("Could not find user...", ConsoleColor.Red);
                        break;
                    case TaskCanceledException:
                        Custom.WriteLine("The request has timed out", ConsoleColor.Red);
                        break;
                }

                return false;
            }

            this.githubUser = JsonSerializer.Deserialize<GitHubUser>(response);

            return true;
        }

        public async Task<IUser> GetUser(string userName){
            var doesExist = await UserExists(userName);
            if (!doesExist)
                return default;

            return this.githubUser;
        }
    }
}