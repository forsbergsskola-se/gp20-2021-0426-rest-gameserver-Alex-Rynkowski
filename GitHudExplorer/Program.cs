using System;
using System.Collections.Generic;
using GitHudExplorer.UserData;
using GitHudExplorer.Utilities;

namespace GitHudExplorer{
    class Program{
        static readonly HashSet<string> IgnoreKeyWords = new(){"components", "assets", "login", "logout", "shop"};
        const string Language = "en-us";

        static void Main(string[] args){
            Run();
            while (true){
                
            }
        }

        static async void Run(){
            IGitHubApi gitHubApi = new GitHubApi();

            while (true){
                Custom.WriteLine("Enter the user you want to lookup...", ConsoleColor.Green);
                var userInput = Custom.ReadLine(ConsoleColor.Yellow);
                await gitHubApi.GetUser(userInput);
            }
        }
    }
}