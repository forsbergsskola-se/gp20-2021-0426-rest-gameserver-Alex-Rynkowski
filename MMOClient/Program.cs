using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.UserStrategy;
using Client.Utilities;

//Color codes:
// Output White
// Yellow instructions
// Green user input
// red error or exception
namespace Client{
    class Program{
        static async Task Main(string[] args){
            var playerStrategy = new PlayerStrategy();
            await RestApi.Api.DeleteRequest<Player>("/drop/playerCollection");
            playerStrategy.ChooseCharacter();
            while (true){
                
            }
        }
    }
}