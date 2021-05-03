using System;

namespace GitHudExplorer.UserData{
    public class NoRepositoriesFoundException : Exception{
        public NoRepositoriesFoundException(){
        }

        public NoRepositoriesFoundException(string message) : base(message){
        }
    }
}