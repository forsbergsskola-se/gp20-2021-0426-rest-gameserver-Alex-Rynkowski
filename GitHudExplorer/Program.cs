using System.Collections.Generic;

namespace GitHudExplorer{
    class Program{
        static readonly HashSet<string> IgnoreKeyWords = new(){"components", "assets", "login", "logout", "shop"};
        const string Language = "en-us";

        static void Main(string[] args){
            IGitHubApi gitHubApi = new GitHubApi();
            gitHubApi.GetUser("Alex Rynkowski");
            while (true){
            }
        }


    }
}