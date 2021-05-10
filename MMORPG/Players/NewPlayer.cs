using System;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG{
    public class NewPlayer{
        readonly string name;
        readonly IMongoCollection<BsonDocument> collection;

        public NewPlayer(string name){
            this.name = name;
            this.collection = ApiUtility.GetDatabase().GetCollection<BsonDocument>("Utilities");
        }

        public void SetupNewPlayer(Player player){
            player.Name = this.name;
            player.Id = Guid.NewGuid();
            player.Score = 0;
            player.Level = 0;
            player.IsDeleted = false;
            player.CreationTime = DateTime.Now;
            player.CurrentExperience = 0;
            player.ExperienceToNextLevel = 100;
        }
    }
}