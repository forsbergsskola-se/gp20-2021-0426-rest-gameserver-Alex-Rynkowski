using System.Linq;
using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class PlayerTests{
        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        [Test]
        public async Task CreateAlexTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player = await Player.Create(P1);
            Assert.AreEqual(P1, player.Name);
            Assert.AreEqual(0, player.Level);
            Assert.AreEqual(0, player.Score);
            Assert.AreEqual(0, player.CurrentExperience);
            Assert.AreEqual(100, player.ExperienceToNextLevel);
            Assert.AreEqual(false, player.IsDeleted);
        }

        [Test]
        public async Task FindPlayerByNameTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player1 = await Player.Create(P1);
            var player2 = await Player.Create(P2);
            var player3 = await Player.Create(P3);

            var playerTwo = await Player.Get(P2);
            Assert.AreEqual(playerTwo.Id, player2.Id);
        }

        [Test]
        public async Task PlayerByIdTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player1 = await Player.Create(P1);
            var player2 = await Player.Create(P2);
            var player3 = await Player.Create(P3);

            var playerOne = await Player.Get(player1.Id);
            Assert.AreEqual(playerOne.Id, player1.Id);
        }

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
    }
}