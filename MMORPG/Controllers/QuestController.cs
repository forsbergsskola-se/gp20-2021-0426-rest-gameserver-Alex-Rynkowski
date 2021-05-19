using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("players/{playerId}/quests/getAllQuests")]
        public Task<Quest[]> GetAllQuests(Guid playerId)
            => this.repository.QuestRepository.GetAllQuests(playerId);

        [HttpPost("players/{id:guid}/quests/complete")]
        public Task<Quest> CompleteQuest(Guid id, Quest quest)
            => this.repository.QuestRepository.CompleteQuest(id, quest);
    }
}