using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.UserData{
    class GitHubApi : IGitHubApi{
        async Task ConnectToGitHub(string user){
            var newUser = user.Replace(' ', '-');
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Token", "sda");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "_userAgent");

            string response;
            try{
                httpClient.Timeout = TimeSpan.FromSeconds(2f);
                response = await httpClient.GetStringAsync($"https://api.github.com/users/{newUser}");
            }
            catch (Exception ex){
                switch (ex){
                    case InvalidOperationException:
                        Custom.WriteLine("Invalid address", ConsoleColor.Red);
                        break;
                    case HttpRequestException:
                        Custom.WriteLine("User does not exist", ConsoleColor.Red);
                        break;
                    case TaskCanceledException:
                        Custom.WriteLine("The request has timed out", ConsoleColor.Red);
                        break;
                }

                return;
            }

            //TODO remove stuff after this line
            var responseArray = response.Remove(response.Length - 1).Substring(1).Replace("\"", string.Empty)
                .Split(',');

            foreach (var s in responseArray){
                var str = s.Split(':', 2);
                str[0] += ":";
                var pad = str[0].PadRight(25, '-');
                Console.Write(pad);
                Console.WriteLine($"{str[1]}");
            }
        }

        public async Task<IUser> GetUser(string userName){
            await ConnectToGitHub(userName);
            return new GitHubUser(userName);
        }
    }
}