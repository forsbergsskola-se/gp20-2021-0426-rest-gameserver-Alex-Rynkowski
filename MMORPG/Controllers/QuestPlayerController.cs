using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/Testing")]
    public class QuestPlayerController{
        readonly IRepository repository;

        public QuestPlayerController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet("ReceiveRandomQuest")]
        public void ReceiveNewQuest()
            => this.repository.AssignQuestInterval();

        [HttpPost("CompleteQuest/{id:guid}/{questName}")]
        public Task<Player> CompleteQuest(Guid id, string questName)
            => this.repository.CompleteQuest(id, questName);
    }
}