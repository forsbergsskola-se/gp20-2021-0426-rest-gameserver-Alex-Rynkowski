using Client.Model;

namespace Client.RestApi{
    public class CurrentPlayer{
        readonly Player player;

        public CurrentPlayer(Player player){
            this.player = player;
        }
    }
}