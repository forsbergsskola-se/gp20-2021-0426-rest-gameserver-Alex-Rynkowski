using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Model;

namespace Client.RestApi{
    public class LeaderboardRequest{
        public static async Task<List<Leaderboard>> GetTopTenByLevel()
            => await Api.GetResponse<List<Leaderboard>>($"players/top-ten-by-level");
        
        public static async Task<List<Leaderboard>> GetTopTenByGold()
            => await Api.GetResponse<List<Leaderboard>>($"players/top-ten-by-gold");
    }
}