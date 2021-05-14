using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MMORPG.Database;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Api{
    public class MongoDbQuestRepository : IQuestRepository{
        const int QuestIntervalSeconds = 10;

        public async Task<Quest> CreateQuest(string questName, int levelRequirement){
            var quest = new NewQuest(questName, levelRequirement).SetupNewQuest();
            await ApiUtility.GetQuestCollection().InsertOneAsync(quest);
            return quest;
        }

        public async Task<Quest> GetQuest(Guid questId){
            var filter = Builders<Quest>.Filter.Eq(x => x.QuestId, questId);
            return await ApiUtility.GetQuestCollection().Find(filter).SingleAsync();
        }

        public async Task<Quest[]> GetAllQuests(){
            var allQuests = await ApiUtility.GetQuestCollection().Find(_ => true).ToListAsync();
            return allQuests.ToArray();
        }


        //TODO refactor this method, it's kind of unreadable atm
        public void AssignQuestInterval(){
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
            GenerateNewQuests(pipeline, playersPipeline);
        }

        static void GenerateNewQuests(BsonDocument[] pipeline, BsonDocument[] playersPipeline){
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

                        Thread.Sleep(TimeSpan.FromSeconds(QuestIntervalSeconds));
                        index++;
                        if (index > allPlayers.Select(x => x).First().Quests.Length - 1){
                            index = 0;
                        }
                    }
                }
            ).Start();
        }

        public async Task<Player> CompleteQuest(Guid id, string questName){
            var match = new BsonDocument{
                {
                    "$match", new BsonDocument{
                        {"QuestName", questName}
                    }
                }
            };
            var pipeline = new[]{match};
            var player = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).FirstAsync();
            var questAgg = await ApiUtility.GetQuestCollection().AggregateAsync<Quest>(pipeline);
            var quest = questAgg.First();


            try{
                player.Quests.First(q => q.QuestName == questName);
            }
            catch (InvalidOperationException e){
                throw new InvalidOperationException("Player does not have that quest " + e);
            }

            if (player.Level < quest.LevelRequirement)
                throw new Exception("Not high enough level to complete quest");


            var filter = Builders<Player>.Filter.ElemMatch(p => p.Quests, q => q.QuestName == questName);
            var updateQuest =
                Builders<Player>.Update.Set(x => x.Quests[-1], null);

            var update = Builders<Player>.Update.Inc(g => g.Gold, quest.GoldReward);
            await AwardExp(id, quest.ExpReward);
            await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(filter, updateQuest);

            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{ReturnDocument = ReturnDocument.After});
        }

        async Task AwardExp(Guid id, int expToGive){
            var increment = Builders<Player>.Update.Inc(x => x.CurrentExperience, expToGive);

            var player = await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), increment,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });

            if (player.CurrentExperience > player.ExperienceToNextLevel){
                var update = Builders<Player>.Update.Set(x => x.CurrentExperience, player.ExperienceToNextLevel);
                await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update);
            }
        }
    }
}