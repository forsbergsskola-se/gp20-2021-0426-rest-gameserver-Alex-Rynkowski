using System;
using System.Net.Http;
using DefaultNamespace;

namespace GitHudExplorer{
    class GitHubApi : IGitHubApi{
        async void ConnectToGitHub(string user){
            var newUser = user.Replace(' ', '-');
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Token", "sda");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "_userAgent");
            var response = await httpClient.GetStringAsync($"https://api.github.com/users/{newUser}");
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

        public IUser GetUser(string userName){
            ConnectToGitHub(userName);
            return new GitHubUser(userName);
        }
    }
}