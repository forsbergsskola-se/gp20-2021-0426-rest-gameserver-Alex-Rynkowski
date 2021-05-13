using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;

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
    }
}