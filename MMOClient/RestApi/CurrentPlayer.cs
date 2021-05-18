using Client.Model;

namespace Client.Api{
    public class CurrentPlayer{
        readonly Player player;

        public CurrentPlayer(Player player){
            this.player = player;
        }
    }
}