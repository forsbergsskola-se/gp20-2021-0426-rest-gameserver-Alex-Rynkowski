using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class UserStrategy{
        public async Task User(){
            IGitHubApi gitHubApi = new GitHubApi();
            while (true){
                var user = await gitHubApi.GetUser("/user");
                var repositories = await user.Repository(new UserRepository()).GetRepositories(user.Login);
                PrintOutAllRepositories(repositories);
                Custom.WriteLine($"Welcome {user.Name} from {user.Location} -\"{user.Company}\"", ConsoleColor.Blue);
                Custom.WriteLine(
                    $"Which repository would you like to investigate? Give me an integer between 0-{repositories.Count - 1}",
                    ConsoleColor.Yellow);
                var userInput = Custom.ReadLine(ConsoleColor.Green);

                var tryParse = int.TryParse(userInput, out var result);

                if (!tryParse || result > repositories.Count){
                    Custom.WriteLine("Invalid input", ConsoleColor.Red);
                }

                await RepositoryIssues(repositories, result, user);
            }
        }

        static async Task RepositoryIssues(Dictionary<int, string> repositories, int result, IUser user){
            while (true){
                Custom.WriteLine($"Selected repository: {repositories[result]}", ConsoleColor.Green);
                Console.WriteLine("\"0\": go back\n\"1\": Get list of all issues \n\"2\": Create a new issue");
                var userInput = Custom.ReadLine(ConsoleColor.Green);

                switch (userInput){
                    case "0":
                        return;
                    case "1":{
                        IAllIssues allIssue = new AllIssues();
                        await allIssue.GetIssuesList(user.Login, repositories[result]);
                        break;
                    }
                    case "2":{
                        IIssue issue = new Issue();
                        await issue.CreateIssue(user.Login, repositories[result]);
                        break;
                    }
                    default:{
                        Custom.WriteLine("Invalid input", ConsoleColor.Red);
                        break;
                    }
                }
            }
        }

        static void PrintOutAllRepositories(Dictionary<int, string> repositories){
            foreach (var (key, value) in repositories){
                Custom.WriteLine($"{key}: {value}", ConsoleColor.White);
            }
        }
    }
}