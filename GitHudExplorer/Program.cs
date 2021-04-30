using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var userInfo = new Dictionary<string, string>();
            IGitHubApi gitHubApi = new GitHubApi(userInfo);

            while (true){
                Custom.WriteLine("Enter the user you want to lookup...", ConsoleColor.Green);
                var userInput = Custom.ReadLine(ConsoleColor.Yellow);

                var user = await gitHubApi.GetUser(userInput);
                if (user == null) continue;
                Custom.WriteLine(user.Description, ConsoleColor.Cyan);

                await AskForInput(userInfo, user);
            }
        }

        static async Task AskForInput(Dictionary<string, string> userInfo, IUser user){
            while (true){
                Custom.WriteLine("What would you like to do?", ConsoleColor.DarkBlue);
                Console.WriteLine("0: Search for new user");
                Console.WriteLine("1: Check repo");

                var userInput = Console.ReadLine();
                if (userInput == "0") break;

                if (userInput == "1"){
                    var repoInfo = new Dictionary<string, string>();
                    var repo = await user.Repository(userInfo["repos_url"]).GetRepositoryList(userInfo["repos_url"]);
                }
            }
        }
    }
}