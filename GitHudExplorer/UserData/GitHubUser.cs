namespace GitHudExplorer{
    public class GitHubUser: IUser{

        readonly string user;
        public GitHubUser(string user){
            this.user = user;
        }

        public string Name{ get => this.user; }
        public string Description{ get; }
    }
}