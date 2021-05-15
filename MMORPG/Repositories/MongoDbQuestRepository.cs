using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Exceptions;
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
            GenerateNewQuests();
        }

        static void GenerateNewQuests(){
            new Thread(async () => {
                    var index = 0;
                    while (true){
                        var quest = await RandomQuestAsync();
                        var players = await GetAllPlayersAsync();
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

        static async Task<List<Player>> GetAllPlayersAsync(){
            var totalPlayers = await ApiUtility.GetPlayerCollection().CountDocumentsAsync(_ => true);
            return await ApiUtility.GetPlayerCollection()
                .Aggregate<Player>(Db.Pipeline("$sample", "size", totalPlayers))
                .ToListAsync();
        }

        static async Task<Quest> RandomQuestAsync(){
            return await ApiUtility.GetQuestCollection()
                .Aggregate<Quest>(Db.Pipeline("$sample", "size", 1)).FirstAsync();
        }

        static int IncrementIndex(int index, List<Player> players){
            index++;
            if (index > players.Select(x => x).First().Quests.Length - 1){
                index = 0;
            }

            return index;
        }

        public async Task<Player> CompleteQuest(Guid id, string questName){
            var player = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).FirstAsync();
            var quest = await GetQuestAsync(questName);

            try{
                player.Quests.First(q => q.QuestName == questName);
            }
            catch (Exception){
                throw new NotFoundException("Player does not have that quest ");
            }

            if (player.Level < quest.LevelRequirement)
                throw new PlayerException("Not high enough level to complete quest");


            var filter = Builders<Player>.Filter.ElemMatch(p => p.Quests, q => q.QuestName == questName);
            var updateQuest =
                Builders<Player>.Update.Set(x => x.Quests[-1], null);

            var update = Builders<Player>.Update.Inc(g => g.Gold, quest.GoldReward);
            await AwardExp(id, quest.ExpReward);
            await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(filter, updateQuest);

            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{ReturnDocument = ReturnDocument.After});
        }

        static async Task<Quest> GetQuestAsync(string questName){
            return await ApiUtility.GetQuestCollection().Aggregate<Quest>(Db.Pipeline("$match", "QuestName", questName)).FirstAsync();
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