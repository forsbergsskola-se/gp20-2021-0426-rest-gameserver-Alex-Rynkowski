using System.Threading.Tasks;

public interface IGitHubApi{
    Task<IUser> GetUser(string userName);
}