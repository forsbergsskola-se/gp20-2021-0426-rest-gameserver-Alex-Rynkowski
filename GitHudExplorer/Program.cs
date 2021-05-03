using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHudExplorer.UserData;
using GitHudExplorer.Utilities;

namespace GitHudExplorer{
    class Program{
        static readonly HashSet<string> IgnoreKeyWords = new(){"components", "assets", "login", "logout", "shop"};

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
                Custom.WriteLine("What would you like to do?", ConsoleColor.Yellow);
                Console.WriteLine("0: Search for new user");
                Console.WriteLine("1: Check repo");

                var userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out var userInputResult)){
                    Custom.WriteLine("Input has to be an integer", ConsoleColor.Red);
                    continue;
                }

                switch (userInputResult){
                    case 0:
                        break;
                    case 1:
                        var repo = await user.Repository().GetRepositoryList(userInfo["repos_url"]);
                        foreach (var (key, value) in repo){
                            Custom.WriteLine($"{key}: {value}", ConsoleColor.White);
                        }
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        continue;
                }
            }
        }
    }
}