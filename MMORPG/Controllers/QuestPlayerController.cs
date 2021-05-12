using MMORPG.Api;

namespace MMORPG.Controllers{
    public class QuestPlayerController{
        readonly IRepository repository;

        public QuestPlayerController(IRepository repository){
            this.repository = repository;
        }
    }
}