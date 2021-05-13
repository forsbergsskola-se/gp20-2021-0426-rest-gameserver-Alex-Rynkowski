using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Api;
using MMORPG.Database;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMORPG.Controllers{
    [ApiController]
    [Route("api/Testing")]
    public class QuestPlayerController{
        readonly IRepository repository;

        public QuestPlayerController(IRepository repository){
            this.repository = repository;
        }

        [HttpGet("QuestTest")]
        public async Task<Player> ReceiveNewQuest(Guid id){
            var match = new BsonDocument{
                {
                    "$sample",
                    new BsonDocument{
                        {"size", 1}
                    }
                }
            };
            var pipeline = new[]{match};

            var randomQuest = await ApiUtility.GetQuestCollection().AggregateAsync<Quest>(pipeline);
            var quest = await randomQuest.FirstAsync();

            var update = Builders<Player>.Update.Push(x => x.Quests, quest);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                });
        }
    }
}