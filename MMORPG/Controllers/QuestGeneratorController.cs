using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/players")]
    public class QuestGeneratorController{
        readonly IRepository repository;

        public QuestGeneratorController(IRepository repository){
            this.repository = repository;
        }

        [HttpPost("CreateQuest")]
        public Task<Quest> CreateQuest(string questName, int levelRequirement)
            => this.repository.CreateQuest(questName, levelRequirement);

        [HttpGet("GetQuest")]
        public Task<Quest> GetQuest(Guid id)
            => this.repository.GetQuest(id);

        [HttpGet("GetAllQuests")]
        public Task<Quest[]> GetAllQuests()
            => this.repository.GetAllQuests();
    }
}