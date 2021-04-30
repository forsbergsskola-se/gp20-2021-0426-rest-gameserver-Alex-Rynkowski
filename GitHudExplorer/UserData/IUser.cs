namespace GitHudExplorer.UserData{
    public interface IUser{
        IRepository Repository(string url);
        string User{ get; }
        string Description{ get; }
    }
}