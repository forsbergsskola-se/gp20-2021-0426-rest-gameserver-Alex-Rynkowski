using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController{
        readonly IRepository repository;

        public StatisticsController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet]
        public Task<PlayerStatistics> GetAllPlayers()
            => this.repository.Statistics.GetAllPlayers();
    }
}