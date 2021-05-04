using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.OtherUser{
    public class OtherUserStrategy{
        public async Task User(){
            var userInfo = new Dictionary<string, string>();
            IGitHubApi gitHubApi = new GitHubApi(userInfo);
            while (true){
                Custom.WriteLine("Enter the user you want to lookup...", ConsoleColor.Yellow);
                Console.WriteLine("Go back: \"0\"");
                
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                if (userInput == "0")
                    break;

                userInput = userInput.Replace(' ', '-');
                var user = await gitHubApi.GetUser($"https://api.github.com/users/{userInput}");
                if (user == null) continue;
                Custom.WriteLine(user.Description(), ConsoleColor.Cyan);

                await AskForInput(userInfo, user);
            }
        }

        static async Task AskForInput(Dictionary<string, string> userInfo, IUser user){
            while (true){
                PrintOutChoices();

                var userInput = Console.ReadLine();
                if (!IsInteger(userInput, out var userInputResult)){
                    Custom.WriteLine("Input has to be an integer", ConsoleColor.Red);
                    continue;
                }

                switch (userInputResult){
                    case 0:
                        return;
                    case 1:
                        var repositories = await user.Repository().GetRepositories(userInfo["repos_url"]);
                        Custom.WriteLine("Which repository would you like to investigate?", ConsoleColor.Green);
                        var repository = Custom.ReadLine(ConsoleColor.Yellow);
                        await user.Repository().GetRepository(repositories[Convert.ToInt32(repository)]);
                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        continue;
                }
            }
        }

        static void PrintOutChoices(){
            Custom.WriteLine("What would you like to do?", ConsoleColor.Yellow);
            Console.WriteLine("0: Search for new user");
            Console.WriteLine("1: Check repo");
        }

        static bool IsInteger(string userInput, out int userInputResult){
            return int.TryParse(userInput, out userInputResult);
        }
    }
}