using System;

namespace GitHudExplorer.API{
    public class NoRepositoriesFoundException : Exception{
        public NoRepositoriesFoundException(){
        }

        public NoRepositoriesFoundException(string message) : base(message){
        }
    }
}