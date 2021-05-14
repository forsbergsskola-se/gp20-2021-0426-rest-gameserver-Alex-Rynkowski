using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class QuestPlayerController{
        readonly IRepository repository;

        public QuestPlayerController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("CompleteQuest/{id:guid}/questing/{questName}")]
        public Task<Player> CompleteQuest(Guid id, string questName)
            => this.repository.CompleteQuest(id, questName);
    }
}