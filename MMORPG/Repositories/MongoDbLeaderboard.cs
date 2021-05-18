using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbLeaderboard : ILeaderboard{
        public async Task<List<LeaderboardData>> GetTopTenByLevel(){
            var players = await ApiUtility.GetPlayerCollection().Find(x => !x.IsDeleted).SortByDescending(d => d.Level)
                .Limit(10).ToListAsync();

            return players.Select(player => new LeaderboardData{
                PlayerName = player.Name,
                PlayerGold = player.Gold,
                PlayerLevel = player.Level
            }).ToList();
        }

        public async Task<List<LeaderboardData>> GetTopTenByGold(){
            var players = await ApiUtility.GetPlayerCollection().Find(x => !x.IsDeleted).SortByDescending(d => d.Gold)
                .Limit(10).ToListAsync();

            return players.Select(player => new LeaderboardData{
                PlayerName = player.Name,
                PlayerGold = player.Gold,
                PlayerLevel = player.Level
            }).ToList();
        }
    }
}