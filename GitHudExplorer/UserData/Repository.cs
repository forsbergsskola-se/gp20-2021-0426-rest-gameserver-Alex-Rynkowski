using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHudExplorer.UserData{
    public class Repository : IRepository{
        readonly string repoName;

        public Repository(string repoName){
            this.repoName = repoName;
        }

        public async Task<List<string>> GetRepositoryList(string url){
            var response = await GitHubApi.ResponseFromServer(this.repoName);
            var split = response.Split("},");
            var lst = split.ToList();
            Console.WriteLine(lst.Count);
            return lst;
        }

        public string Name{ get; }
        public string Description{ get; }
    }
}