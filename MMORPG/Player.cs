using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MMORPG{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Player{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Score{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }

        public int CurrentExperience{ get; set; }
        public int ExperienceToNextLevel{ get; set; }
        public IItem Item{ get; set; }

        public IItem GetItems(Guid playerId){
            return this.Item;
        }
    }
}