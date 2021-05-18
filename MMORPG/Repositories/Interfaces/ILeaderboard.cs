using System.Collections.Generic;
using System.Threading.Tasks;
using MMORPG.Data;

namespace MMORPG.Repositories{
    public interface ILeaderboard{
        public Task<List<LeaderboardData>> GetTopTenByLevel();
        public Task<List<LeaderboardData>> GetTopTenByGold();
    }
}