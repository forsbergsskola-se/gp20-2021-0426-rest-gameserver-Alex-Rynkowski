using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHudExplorer.Utilities;

namespace GitHudExplorer.UserData{
    public class Repository : IRepository{
        public async Task<Dictionary<int, string>> GetRepositoryList(string url){
            var dictionaryIndex = 0;
            var repoDictionary = new Dictionary<int, string>();
            var response = await Connection.GetFromUrl(url);
            var split = response.Split("},");

            foreach (var s in split){
                var sp = s.Split("\",");
                foreach (var s1 in sp){
                    var tmp = s1.Split("\":");
                    var tmpZero = tmp[0].Replace("\"", "");
                    var tmpOne = tmp[1].Replace("\"", "");
                    if (tmpZero == "url" && !tmpOne.Contains("licenses")){
                        repoDictionary[dictionaryIndex] = tmpOne;
                        dictionaryIndex++;
                        
                    }
                }
            }

            return repoDictionary;
        }

        public Task GetRepository(string url){
            throw new NotImplementedException();
        }

        public string Name{ get; }
        public string Description{ get; }
    }
}