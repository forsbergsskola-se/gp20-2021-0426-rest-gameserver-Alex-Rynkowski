using System.Threading.Tasks;

namespace GitHudExplorer.API{
    public interface IStrategy{
        Task User();
        Task OtherUser();
    }
}