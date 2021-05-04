using System.Threading.Tasks;
using GitHudExplorer.API;
using GitHudExplorer.OtherUser;
using GitHudExplorer.User;

namespace GitHudExplorer{
    public class Strategy : IStrategy{
        public async Task User(){
            var userStrategy = new UserStrategy();
            await userStrategy.User();
        }

        public async Task OtherUser(){
            var otherUser = new OtherUserStrategy();
            await otherUser.User();
        }
    }
}