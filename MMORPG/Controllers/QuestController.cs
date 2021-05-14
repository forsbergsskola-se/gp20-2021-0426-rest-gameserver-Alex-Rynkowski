using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Data;
using MMORPG.Repositories;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api")]
    public class QuestController{
        readonly IRepository repository;

        public QuestController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("quests/createQuest")]
        public Task<Quest> CreateQuest(string questName, int levelRequirement)
            => this.repository.QuestRepository.CreateQuest(questName, levelRequirement);

        [HttpGet("quests/getQuest/{questId:guid}")]
        public Task<Quest> GetQuest(Guid questId)
            => this.repository.QuestRepository.GetQuest(questId);

        [HttpGet("quests/getAllQuests")]
        public Task<Quest[]> GetAllQuests()
            => this.repository.QuestRepository.GetAllQuests();

        [HttpGet("quests/delegateRandomQuest")]
        public void DelegateNewQuest()
            => this.repository.QuestRepository.AssignQuestInterval();
        
        [HttpPost("CompleteQuest/{id:guid}/questing/{questName}")]
        public Task<Player> CompleteQuest(Guid id, string questName)
            => this.repository.QuestRepository.CompleteQuest(id, questName);
    }
}