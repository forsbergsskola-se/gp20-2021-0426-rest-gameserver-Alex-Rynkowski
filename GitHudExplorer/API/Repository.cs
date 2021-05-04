using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.API{
    public class Repository : IRepository{
        public async Task<Dictionary<int, string>> GetRepositories(string url){
            var dictionaryIndex = 0;
            var repoDictionary = new Dictionary<int, string>();
            var response = await Connection.GetFromUrl(url);
            var classes = response.Split("},");

            foreach (var _class in classes){
                var splitClasses = _class.Split("\",");
                foreach (var splitClass in splitClasses){
                    var splitValue = splitClass.Split("\":");
                    try{
                        var header = splitValue[0][5..].Replace("\"", "");
                        var description = splitValue[1].Replace("\"", "");
                        if (header == "forks_url" && !description.Contains("licenses")){
                            repoDictionary[dictionaryIndex] = GetLink(description);
                            Custom.WriteLine($"{dictionaryIndex} {GetRepositoryName(repoDictionary[dictionaryIndex])}",
                                ConsoleColor.White);
                            dictionaryIndex++;
                        }
                    }
                    catch (Exception){
                        throw new NoRepositoriesFoundException("No repositories found");
                    }
                }
            }

            return repoDictionary;
        }

        string GetLink(string repository){
            var lastIndex = repository.LastIndexOf('/');
            return repository.Remove(lastIndex);
        }

        string GetRepositoryName(string repositoryUrl){
            var lastIndex = repositoryUrl.LastIndexOf('/') + 1;
            return repositoryUrl[lastIndex..];
        }

        public async Task GetRepository(string url){
            var response = await Connection.GetFromUrl(url);
            Console.WriteLine(response);
        }

        public string Name{ get; }
        public string Description{ get; }
    }
}