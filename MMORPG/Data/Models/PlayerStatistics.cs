using System.Collections.Generic;

namespace MMORPG.Data{
    public class PlayerStatistics{
        public int TotalPlayersAmount{ get; set; }
        public List<Statistics> Statistics{ get; set; }
    }

    public class Statistics{
        public int Gold{ get; set; }
        public int ItemsAmount{ get; set; }
        public int Level{ get; set; }
    }
}