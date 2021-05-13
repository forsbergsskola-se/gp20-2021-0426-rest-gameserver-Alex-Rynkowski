using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        [HttpGet("ReceiveRandomQuest")]
        public void ReceiveNewQuest(){
            var totalPlayers = ApiUtility.GetPlayerCollection().CountDocuments(_ => true);
            var size = new BsonDocument{
                {
                    "$sample", new BsonDocument{
                        {"size", (int) totalPlayers}
                    }
                }
            };

            var match = new BsonDocument{
                {
                    "$sample", new BsonDocument{
                        {"size", 1}
                    }
                }
            };
            var pipeline = new[]{match};
            var playersPipeline = new[]{size};
            new Thread(async () => {
                    var index = 0;
                    while (true){
                        var randomQuest = await ApiUtility.GetQuestCollection().AggregateAsync<Quest>(pipeline);
                        var players = await ApiUtility.GetPlayerCollection().AggregateAsync<Player>(playersPipeline);
                        var quest = await randomQuest.FirstAsync();
                        var allPlayers = await players.ToListAsync();
                        Console.WriteLine();
                        var update = Builders<Player>.Update.Set(x => x.Quests[index], quest);
                        Console.WriteLine($"Time: {DateTime.Now.TimeOfDay}");
                        foreach (var p in allPlayers){
                            var filter = Builders<Player>.Filter.Eq(nameof(p.Id), p.Id);
                            var player = await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(
                                filter, update,
                                new FindOneAndUpdateOptions<Player>{
                                    ReturnDocument = ReturnDocument.After,
                                    IsUpsert = true
                                });
                            Console.WriteLine(
                                $"Quest at index: {index}: {player.Quests[index].QuestName}, assigned to player: {p.Name}, level requirement: {player.Quests[index].LevelRequirement}");
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        index++;
                        if (index > allPlayers.Select(x => x).First().Quests.Length - 1){
                            index = 0;
                        }
                    }
                }
            ).Start();
        }
    }
}