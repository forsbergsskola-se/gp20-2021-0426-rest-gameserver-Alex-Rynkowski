﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MMORPG.Database;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.Api{
    public class MongoDbRepository : IRepository{
        const int QuestIntervalSeconds = 10;

        public async Task<Player> Get(Guid id){
            var foundPlayer = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).SingleAsync();
            if (!foundPlayer.IsDeleted) return foundPlayer;

            throw new NotFoundException("Player does not exist or has been deleted");
        }

        public async Task<Player> GetPlayerByName(string name){
            var match = new BsonDocument{
                {
                    "$match", new BsonDocument{
                        {"Name", name}
                    }
                }
            };
            var pipeline = new[]{match};
            var playerAgg = await ApiUtility.GetPlayerCollection().AggregateAsync<Player>(pipeline);
            return playerAgg.ToList().First();
        }

        public async Task<Player[]> GetAll(){
            var allPlayers = await ApiUtility.GetPlayerCollection().Find(_ => true).ToListAsync();
            return allPlayers.Select(player => player)
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        public async Task<Player> Create(string name){
            var exists = await ApiUtility.GetPlayerCollection().Find(_ => _.Name == name).AnyAsync();
            if (exists)
                throw new Exception("Player with that name already exists");

            var player = new NewPlayer(name).SetupNewPlayer(new Player());
            await SendPlayerDataToMongo(player);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            await ApiUtility.GetPlayerCollection().InsertOneAsync(player);
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            var update = Builders<Player>.Update
                .Set(x => x.Gold, modifiedPlayer.Gold)
                .Set(x => x.Score, modifiedPlayer.Score)
                .Set(x => x.Level, modifiedPlayer.Level);

            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }


        public async Task<Player> Delete(Guid id){
            var update = Builders<Player>.Update.Set("IsDeleted", true);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Player> PurchaseLevel(Guid id){
            var player = await Get(id);
            if (player.CurrentExperience < player.ExperienceToNextLevel)
                throw new Exception("Not enough experience");
            if (!Calculate.CanAffordLevel(player.Level, player.Gold))
                throw new Exception("Not enough gold");

            player.Gold -= (player.Level + 1) * 100;
            player.Level++;
            var update = Builders<Player>.Update
                .Inc(l => l.Level, player.Level)
                .Set(g => g.Gold, player.Gold)
                .Set(e => e.CurrentExperience, 0);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Player> CreateItem(Guid id, string itemName, ItemTypes itemType){
            if (!IsCorrectItemType(itemType)){
                throw new Exception("Item type does not exist");
            }

            var item = new Item(itemName, itemType.ToString());

            var update = Builders<Player>.Update.Push(x => x.Inventory, item);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(id.GetPlayerById(), update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                });
        }

        static bool IsCorrectItemType(ItemTypes itemType){
            return Enum.IsDefined(typeof(ItemTypes), itemType.ToString());
        }

        public async Task<Item> DeleteItem(Guid id, string itemName){
            var filter = Builders<Player>.Filter.And(id.GetPlayerById(),
                Builders<Player>.Filter.ElemMatch(x => x.Inventory, x => x.ItemName == itemName));

            var update = Builders<Player>.Update.Set("Inventory.$.IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(filter, update);
            return default;
        }

        public async Task<List<Item>> GetInventory(Guid id){
            try{
                var inventory = await ApiUtility.GetPlayerCollection().Find(Db.GetPlayerById(id)).SingleAsync();
                return inventory.Inventory.Where(x => !x.IsDeleted).Select(item => item).ToList();
            }
            catch (Exception e){
                throw new NotFoundException("Player not found or has been deleted " + e);
            }
        }

        public async Task<Item> GetItem(Guid id, string name){
            var inventory = await GetInventory(id);
            return inventory.Find(item => item.ItemName == name);
        }

        public async Task<Item> SellItem(Guid id, string itemName){
            var item = await GetItem(id, itemName);
            var equippedItems = await ApiUtility.GetPlayerCollection()
                .FindAsync(x => x.EquippedItems[item.ItemType] == item);
            if (equippedItems != null)
                await UnEquip(id, itemName);

            var update = Builders<Player>.Update.Inc(x => x.Gold, item.SellValue);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(id.GetPlayerById(), update, new UpdateOptions{IsUpsert = true});
            return await DeleteItem(id, itemName);
        }

        public async Task<Item> EquipSword(Guid id, string weaponName)
            => await Equip(id, weaponName, ItemTypes.Sword);

        public async Task<Item> EquipShield(Guid id, string shieldName)
            => await Equip(id, shieldName, ItemTypes.Shield);

        public async Task<Item> EquipArmor(Guid id, string armorName)
            => await Equip(id, armorName, ItemTypes.Armor);

        public async Task<Item> EquipHelmet(Guid id, string helmetName)
            => await Equip(id, helmetName, ItemTypes.Helmet);

        async Task<Item> Equip(Guid id, string name, ItemTypes type){
            var item = await GetItem(id, name);
            if (IsNullOrWrongType(type, item))
                throw new NotFoundException("Item not found in player inventory");

            var player = await Get(id);
            //TODO implement custom exception
            if (player.Level < item.LevelRequirement)
                throw new Exception("Level not high enough");

            await UnEquip(id, name);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType], item)
                .Inc(x => x.Level, item.LevelBonus);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(id.GetPlayerById(), update, new UpdateOptions{IsUpsert = true});
            return item;
        }

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

        async Task UnEquip(Guid id, string name){
            var item = await GetItem(id, name);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType], null)
                .Inc(player => player.Level, -item.LevelBonus);

            await ApiUtility.GetPlayerCollection().UpdateOneAsync(id.GetPlayerById(), update);
        }

        static bool IsNullOrWrongType(ItemTypes type, Item item){
            return item == null || item.ItemType != type.ToString();
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
            var player = await Get(id);
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