using MMORPG.Api;

namespace MMORPG.Controllers{
    public class LevelController{
        readonly IRepository repository;

        public LevelController(IRepository repository){
            this.repository = repository;
        }
        
        
    }
}