using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api")]
    public class QuestGeneratorController{
        readonly IRepository repository;

        public QuestGeneratorController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("quests/createQuest")]
        public Task<Quest> CreateQuest(string questName, int levelRequirement)
            => this.repository.CreateQuest(questName, levelRequirement);

        [HttpGet("quests/getQuest/{questId:guid}")]
        public Task<Quest> GetQuest(Guid questId)
            => this.repository.GetQuest(questId);

        [HttpGet("quests/getAllQuests")]
        public Task<Quest[]> GetAllQuests()
            => this.repository.GetAllQuests();

        [HttpGet("quests/delegateRandomQuest")]
        public void DelegateNewQuest()
            => this.repository.AssignQuestInterval();
    }
}