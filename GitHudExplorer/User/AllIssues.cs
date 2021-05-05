using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.User{
    public class AllIssues : IAllIssues{
        public async Task<List<string>> GetIssuesList(string userName, string repositoryName){
            var response = await Connection.GetFromUrl($"/repos/{userName}/{repositoryName}/issues");

            try{
                var responseList = JsonSerializer.Deserialize<List<Issue>>(response);

                foreach (var issue in responseList){
                    Custom.WriteLine($"Issue \n{issue.Title}\n", ConsoleColor.White);
                    Console.WriteLine($"Body: \n{issue.Body}");
                    Console.WriteLine("--------------");
                }
            }
            catch (Exception){
                Custom.WriteLine($"No issues found for {repositoryName}", ConsoleColor.Red);
            }
            
            return default;
        }

        public IIssue Issue(){
            return new Issue();
        }
    }
}