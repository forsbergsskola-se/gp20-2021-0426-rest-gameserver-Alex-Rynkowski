namespace GitHudExplorer.UserData{
    public interface IUser{
        IRepository Repository();
        string User{ get; }
        string Description{ get; }
    }
}