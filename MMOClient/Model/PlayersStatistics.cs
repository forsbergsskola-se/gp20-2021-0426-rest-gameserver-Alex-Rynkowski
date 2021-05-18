using System.Collections.Generic;

namespace Client.Model{
    public class PlayersStatistics{
        public int TotalPlayersAmount{ get; set; }
        public List<Statistics> Statistics{ get; set; }
    }

    public class Statistics{
        public int Gold{ get; set; }
        public int ItemsAmount{ get; set; }
        public int Level{ get; set; }
    }
}