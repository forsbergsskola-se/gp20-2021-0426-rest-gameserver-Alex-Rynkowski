using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class LeaderboardStrategy{
        public async Task Leaderboard(){
            while (true){
                Custom.WriteMultiLines(ConsoleColor.Yellow, "0: Go back", "1: Get top ten by level",
                    "2: Get top ten by gold");
                var userInput = Custom.ReadLine(ConsoleColor.Green);
                switch (userInput){
                    case "0":
                        return;
                    case "1":
                        var topTenByLevel = await GetTopTenByLevel();
                        for (var i = 0; i < topTenByLevel.Count; i++){
                            var leaderboard = topTenByLevel[i];
                            Custom.WriteMultiLines(ConsoleColor.White, $"{i + 1}: {leaderboard.PlayerName}",
                                $"Level: {leaderboard.PlayerLevel}", $"Gold: {leaderboard.PlayerGold}");
                            Console.WriteLine("-----------------------");
                        }

                        break;
                    case "2":
                        var topTenByGold = await GetTopTenByGold();
                        for (var i = 0; i < topTenByGold.Count; i++){
                            var leaderboard = topTenByGold[i];
                            Custom.WriteMultiLines(ConsoleColor.White, $"{i + 1}: {leaderboard.PlayerName}",
                                $"Level: {leaderboard.PlayerGold}", $"Gold: {leaderboard.PlayerLevel}");
                            Console.WriteLine("-----------------------");
                        }

                        break;
                    default:
                        Custom.WriteLine("Unknown input", ConsoleColor.Red);
                        break;
                }
            }
        }

        async Task<List<Leaderboard>> GetTopTenByGold()
            => await LeaderboardRequest.GetTopTenByGold();

        async Task<List<Leaderboard>> GetTopTenByLevel()
            => await LeaderboardRequest.GetTopTenByLevel();
    }
}