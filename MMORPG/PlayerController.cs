namespace MMORPG{
    public class PlayerController{
        IRepository repository;

        public PlayerController(IRepository repository){
            this.repository = repository;
        }
    }
}