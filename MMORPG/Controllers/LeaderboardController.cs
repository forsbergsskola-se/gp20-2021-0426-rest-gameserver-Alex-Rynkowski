using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class LeaderboardController{
        readonly IRepository repository;

        public LeaderboardController(IRepository repository){
            this.repository = repository;
        }
        
        [HttpGet("top-ten-by-level")]
        public async Task<List<LeaderboardData>> GetTopTenByLevel()
            => await this.repository.Leaderboard.GetTopTenByLevel();
        
        [HttpGet("top-ten-by-gold")]
        public async Task<List<LeaderboardData>> GetTopTenByGold()
            => await this.repository.Leaderboard.GetTopTenByGold();
    }
}