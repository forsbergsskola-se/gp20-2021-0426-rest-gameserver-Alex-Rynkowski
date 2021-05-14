using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.BLL{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Quest{
        public Guid QuestId{ get; set; }
        public string QuestName{ get; set; }
        public int LevelRequirement{ get; set; }

        public int ExpReward{ get; set; }

        //public Item ItemReward{ get; set; }
        public int GoldReward{ get; set; }
    }
}