using System;
using System.Collections.Generic;
using MMORPG.Items;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG.Players{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Player{
        public string Id{ get; set; }
        public string Name{ get; set; }
        public int Score{ get; set; }
        public int Gold{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public int CurrentExperience{ get; set; }
        public int ExperienceToNextLevel{ get; set; }
        public List<Item> Inventory{ get; set; }

        public Dictionary<string, Item> EquippedItems{ get; set; }
    }
}