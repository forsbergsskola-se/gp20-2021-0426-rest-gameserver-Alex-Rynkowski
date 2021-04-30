using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.UserData{
    class GitHubApi : IGitHubApi{
        readonly Dictionary<string, string> userInfo;

        public GitHubApi(Dictionary<string, string> userInfo){
            this.userInfo = userInfo;
        }

        public static async Task<string> ResponseFromServer(string url){
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Token", "sda");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "_userAgent");

            httpClient.Timeout = TimeSpan.FromSeconds(2f);
            return await httpClient.GetStringAsync(url);
        }

        async Task<bool> UserExists(string user){
            var newUser = user.Replace(' ', '-');
            string response;
            try{
                response = await ResponseFromServer($"https://api.github.com/users/{newUser}");
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

            //TODO remove stuff after this line
            var responseArray = response.Remove(response.Length - 1).Substring(1).Replace("\"", string.Empty)
                .Split(',');

            foreach (var s in responseArray){
                var str = s.Split(':', 2);
                this.userInfo[str[0]] = str[1];
            }

            return true;
        }

        public async Task<IUser> GetUser(string userName){
            var doesExist = await UserExists(userName);
            if (!doesExist)
                return default;

            return new GitHubUser(this.userInfo, userName);
        }
    }
}