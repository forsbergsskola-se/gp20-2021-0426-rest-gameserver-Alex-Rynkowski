using System.Threading.Tasks;
using Client.Model;
using Client.Requests;
using Client.RestApi;
using NUnit.Framework;

namespace MMO.Test{
    public class StatisticsTest{
        Player player1;
        Player player2;
        Player player3;

        const string P1 = "Alex R";
        const string P2 = "Marc Z";
        const string P3 = "Raphael A";

        [Test]
        public static async Task GetTotalAmountOfPlayersTest(){
            var stats = await StatisticsRequest.GetStatistics();

            Assert.AreEqual(3, stats.TotalPlayersAmount);
        }

        [Test]
        public async Task GetTotalAmountOfGoldFromPlayersTest(){
            await SetupTest();

            this.player1 = await PlayerResponse.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 1000,
            });
            this.player2 = await PlayerResponse.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 500,
            });
            this.player3 = await PlayerResponse.Modify(this.player3.Id, new ModifiedPlayer{
                Gold = 600,
            });

            var stats = await StatisticsRequest.GetStatistics();
            Assert.AreEqual(2100, stats.Gold);
        }
        
        [Test]
        public async Task GetTotalAmountOfLevelFromPlayersTest(){
            await SetupTest();

            this.player1 = await PlayerResponse.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = 1000,
            });
            this.player2 = await PlayerResponse.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = 500,
            });
            this.player3 = await PlayerResponse.Modify(this.player3.Id, new ModifiedPlayer{
                Gold = 600,
            });

            var stats = await StatisticsRequest.GetStatistics();
            Assert.AreEqual(2100, stats.Gold);
        }

        async Task SetupTest(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await PlayerResponse.Create(P1);
            this.player2 = await PlayerResponse.Create(P2);
            this.player3 = await PlayerResponse.Create(P3);
        }
    }
}