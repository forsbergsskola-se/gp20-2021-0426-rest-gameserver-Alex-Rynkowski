using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbQuestRepository : IQuestRepository{
        const int QuestIntervalSeconds = 10;
        static IPlayerRepository PlayerRepository => new MongoDbPlayerRepository();

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


        public void AssignQuestInterval(){
            var totalPlayers = ApiUtility.GetPlayerCollection().CountDocuments(_ => true);
            var playersPipeline = PlayersPipeline(totalPlayers);
            var pipeline = PipeLine();
            GenerateNewQuests(pipeline, playersPipeline);
        }

        static void GenerateNewQuests(BsonDocument[] pipeline, BsonDocument[] playersPipeline){
            new Thread(async () => {
                    var index = 0;
                    while (true){
                        var quest = await ApiUtility.GetQuestCollection().Aggregate<Quest>(pipeline).FirstAsync();
                        var players = await ApiUtility.GetPlayerCollection().Aggregate<Player>(playersPipeline)
                            .ToListAsync();
                        var update = Builders<Player>.Update.Set(x => x.Quests[index], quest);
                        foreach (var p in players){
                            await PlayerRepository.UpdatePlayer(p.Id, update);
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(QuestIntervalSeconds));
                        index = IncrementIndex(index, players);
                    }
                }
            ).Start();
        }

        static int IncrementIndex(int index, List<Player> players){
            index++;
            if (index > players.Select(x => x).First().Quests.Length - 1){
                index = 0;
            }

            return index;
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

        static BsonDocument[] PlayersPipeline(long totalPlayers){
            var size = new BsonDocument{
                {
                    "$sample", new BsonDocument{
                        {"size", (int) totalPlayers}
                    }
                }
            };
            var playersPipeline = new[]{size};
            return playersPipeline;
        }

        static BsonDocument[] PipeLine(){
            var match = new BsonDocument{
                {
                    "$sample", new BsonDocument{
                        {"size", 1}
                    }
                }
            };
            var pipeline = new[]{match};
            return pipeline;
        }
    }
}