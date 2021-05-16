using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Data;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbQuestRepository : IQuestRepository{
        const int QuestIntervalSeconds = 3;
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


        public async Task<Player> AssignQuests(Player player, DateTime lastLoginTime){
            var timeDiff = GetSeconds(lastLoginTime) / QuestIntervalSeconds;
            Console.WriteLine($"Assigned quest? {player.Name}");

            for (var i = 0; i < timeDiff; i++){
                var quest = await RandomQuestAsync();
                var update = Builders<Player>.Update
                    .Set(x => x.Quests[player.QuestIndex], quest)
                    .Set(i => i.QuestIndex, player.QuestIndex);

                player = await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(player.Id.GetPlayerById(),
                    update, new FindOneAndUpdateOptions<Player>{
                        ReturnDocument = ReturnDocument.After
                    });
                Console.WriteLine(
                    $"Quest {player.Quests[player.QuestIndex].QuestName} assigned to: {player.Name} at index: {player.QuestIndex}");
                IncrementIndex(player);
            }

            return player;
        }

        int GetSeconds(DateTime lastLogin){
            var calculation = DateTime.Now - lastLogin;
            return calculation.Seconds;
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

        static void IncrementIndex(Player player){
            player.QuestIndex++;
            if (player.QuestIndex > player.Quests.Length - 1){
                player.QuestIndex = 0;
            }
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
            return await ApiUtility.GetQuestCollection().Aggregate<Quest>(Db.Pipeline("$match", "QuestName", questName))
                .FirstAsync();
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