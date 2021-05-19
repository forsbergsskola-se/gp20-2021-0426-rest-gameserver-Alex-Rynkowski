using System;

namespace Client.Model{
    public class Quest{
        public Guid QuestId{ get; set; }
        public string QuestName{ get; set; }
        public int LevelRequirement{ get; set; }
        public int ExpReward{ get; set; }
        public int GoldReward{ get; set; }
    }
}