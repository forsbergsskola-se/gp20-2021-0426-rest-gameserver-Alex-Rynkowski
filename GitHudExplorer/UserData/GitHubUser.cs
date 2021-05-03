using System;
using System.Collections.Generic;

namespace GitHudExplorer.UserData{
    public class GitHubUser : IUser{
        readonly Dictionary<string, string> userInfo;

        public GitHubUser(Dictionary<string, string> userInfo){
            this.userInfo = userInfo;
        }

        public IRepository Repository(){
            var repo = new Repository();
            return repo;
        }

        public string User => this.userInfo["name"];

        public string Description =>
            $"{this.userInfo["name"]} from {this.userInfo["location"]} is working for {this.userInfo["company"]}";
    }
}