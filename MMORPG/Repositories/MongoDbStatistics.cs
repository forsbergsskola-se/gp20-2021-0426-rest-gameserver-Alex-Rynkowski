using System.Linq;
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
                TotalPlayersAmount = (int) await ApiUtility.GetPlayerCollection().CountDocumentsAsync(_ => true)
            };

            foreach (var player in players){
                statistics.Gold += player.Gold;
                statistics.Level += player.Level;
                statistics.ItemsAmount += player.Inventory.Count;
                statistics.ItemsAmount += player.EquippedItems.Count(x => x.Key != null);
            }

            return statistics;
        }
    }
}