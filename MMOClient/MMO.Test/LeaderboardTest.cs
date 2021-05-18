using System;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using NUnit.Framework;

namespace MMO.Test{
    public class LeaderboardTest{
        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Player player6;
        Player player5;
        Player player7;
        Player player8;
        Player player9;
        Player player10;
        Player player11;
        Player player12;

        [Test]
        public async Task GetTopTenByLevelTest(){
            await SetupPlayers();

            var leaderboardList = await LeaderboardRequest.GetTopTenByLevel();

            for (var i = 0; i < leaderboardList.Count - 1; i++){
                Assert.IsTrue(leaderboardList[i].PlayerLevel >= leaderboardList[i + 1].PlayerLevel);
            }
            
            Assert.AreEqual(10, leaderboardList.Count);
        }

        [Test]
        public async Task GetTopTenByGoldTest(){
            await SetupPlayers();

            var leaderboardList = await LeaderboardRequest.GetTopTenByGold();

            for (var i = 0; i < leaderboardList.Count - 1; i++){
                Assert.IsTrue(leaderboardList[i].PlayerGold >= leaderboardList[i + 1].PlayerGold);
            }
            Assert.AreEqual(10, leaderboardList.Count);
        }
        async Task SetupPlayers(){
            await Api.DeleteRequest<Player>("drop/playerCollection");
            this.player1 = await PlayerRequest.Create("P1");
            this.player2 = await PlayerRequest.Create("P2");
            this.player3 = await PlayerRequest.Create("P3");
            this.player4 = await PlayerRequest.Create("P4");
            this.player5 = await PlayerRequest.Create("P5");
            this.player6 = await PlayerRequest.Create("P6");
            this.player7 = await PlayerRequest.Create("P7");
            this.player8 = await PlayerRequest.Create("P8");
            this.player9 = await PlayerRequest.Create("P9");
            this.player10 = await PlayerRequest.Create("P10");
            this.player11 = await PlayerRequest.Create("P11");
            this.player12 = await PlayerRequest.Create("P12");

            await PlayerRequest.Modify(this.player1.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player2.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player3.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player4.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player5.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player6.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player7.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player8.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player9.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player10.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player11.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
            await PlayerRequest.Modify(this.player12.Id, new ModifiedPlayer{
                Gold = new Random().Next(100, 20000),
                Level = new Random().Next(0, 99)
            });
        }
    }
}