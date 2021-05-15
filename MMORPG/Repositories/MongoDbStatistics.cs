using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbStatistics : IStatistics{
        static IRepository Repository => new MongoDbRepository();

        public async Task<PlayerStatistics> GetAllPlayers(){
            var players = await Repository.PlayerRepository.GetAll();
            var statistics = new PlayerStatistics{
                Statistics = new List<Statistics>(),
                TotalPlayersAmount = (int) await ApiUtility.GetPlayerCollection().CountDocumentsAsync(_ => true)
            };

            foreach (var player in players){
                statistics.Statistics.Add(new Statistics{
                    Gold = player.Gold,
                    ItemsAmount = player.Inventory.Count,
                    Level = player.Level
                });
            }

            return statistics;
        }
    }
}