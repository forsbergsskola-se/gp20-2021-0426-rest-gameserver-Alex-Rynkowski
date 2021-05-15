using System.Threading.Tasks;
using Client.Model;
using Client.Utilities;
using NUnit.Framework;

namespace MMO.Test{
    public class CreatePlayerTests{
        [Test]
        public async Task CreateAlexTest(){
            await ApiConnection.DeleteRequest<Player>("drop/playerCollection");
            var player = await Player.Create("Alex R");
            Assert.AreEqual("Alex R", player.Name);
            Assert.AreEqual(0, player.Level);
            Assert.AreEqual(0, player.Score);
            Assert.AreEqual(0, player.CurrentExperience);
            Assert.AreEqual(null, player.InventoryList);
            Assert.AreEqual(100, player.ExperienceToNextLevel);
            Assert.AreEqual(false, player.IsDeleted);
        }

        [Test]
        public async Task CreateMarcTest(){
            var player = await Player.Create("Marc Z");
            Assert.AreEqual("Marc Z", player.Name);
            Assert.AreEqual(0, player.Level);
            Assert.AreEqual(0, player.Score);
            Assert.AreEqual(0, player.CurrentExperience);
            Assert.AreEqual(null, player.InventoryList);
            Assert.AreEqual(100, player.ExperienceToNextLevel);
            Assert.AreEqual(false, player.IsDeleted);
        }

        [Test]
        public async Task CreateRaphaelTest(){
            var player = await Player.Create("Raphael A");
            Assert.AreEqual("Raphael A", player.Name);
            Assert.AreEqual(0, player.Level);
            Assert.AreEqual(0, player.Score);
            Assert.AreEqual(0, player.CurrentExperience);
            Assert.AreEqual(null, player.InventoryList);
            Assert.AreEqual(100, player.ExperienceToNextLevel);
            Assert.AreEqual(false, player.IsDeleted);
        }
    }
}