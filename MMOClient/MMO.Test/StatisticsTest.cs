using System.Threading.Tasks;
using Client.Model;
using Client.Requests;
using Client.RestApi;
using NUnit.Framework;

namespace MMO.Test{
    public static class StatisticsTest{
        static object player1;
        static Player player2;
        static Player player3;

        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        [Test]
        public static async Task GetTotalAmountOfPlayersTest(){
            var stats = await StatisticsRequest.GetStatistics();

            Assert.AreEqual(3, stats.TotalPlayersAmount);
        }
        
        [Test]
        public static async Task GetTotalOfEachPlayerTest(){
            var stats = await StatisticsRequest.GetStatistics();

            
            
            
            Assert.AreEqual(3, stats.TotalPlayersAmount);
            Assert.AreEqual(3, stats.TotalPlayersAmount);
            Assert.AreEqual(3, stats.TotalPlayersAmount);
        }

        static async Task SetupTest(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            player1 = await PlayerResponse.Create(P1);
            player2 = await PlayerResponse.Create(P2);
            player3 = await PlayerResponse.Create(P3);
        }
    }
}