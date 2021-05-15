using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class GetPlayersTest{
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";
        [Test]
        public async Task GetAll(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player1 = await Player.Create(P1);
            var player2 = await Player.Create(P2);
            var player3 = await Player.Create(P3);
            var players = await Player.GetAll();
            Assert.AreEqual(player1.Name, players.Where(x => x.Name == P1).Select(x => x).First().Name);
            Assert.AreEqual(player2.Name, players.Where(x => x.Name == P2).Select(x => x).First().Name);
            Assert.AreEqual(player3.Name, players.Where(x => x.Name == P3).Select(x => x).First().Name);
        }

        [Test]
        public async Task Get(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player1 = await Player.Create(P1);
            var player2 = await Player.Create(P2);
            var player3 = await Player.Create(P3);

            var player = await Player.Get(P2);
            Assert.AreEqual(player.Id, player2.Id);

        }
    }
}