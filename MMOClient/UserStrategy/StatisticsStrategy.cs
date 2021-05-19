using System;
using System.Threading.Tasks;
using Client.Model;
using Client.RestApi;
using Client.Utilities;

namespace Client.UserStrategy{
    public class StatisticsStrategy{
        public async Task Statistics(){
            var stats = await GetStatistics();
            Custom.WriteMultiLines(ConsoleColor.White, $"Total amount of Gold: {stats.Gold}",
                $"Total amount of Items: {stats.ItemsAmount}",
                $"Total level count: {stats.Level}", $"Total amount of players: {stats.TotalPlayersAmount}");
        }

        async Task<PlayersStatistics> GetStatistics()
            => await StatisticsRequest.GetStatistics();
    }
}