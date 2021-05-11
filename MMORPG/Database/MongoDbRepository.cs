using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Api;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Database{
    public class MongoDbRepository : IRepository{
        public async Task<Player> Get(Guid id){
            var foundPlayer = await ApiUtility.GetPlayerCollection().Find(Db.GetPlayerById(id)).SingleAsync();
            if (!foundPlayer.IsDeleted) return foundPlayer;

            throw new NotFoundException("Player does not exist or has been deleted");
        }

        public async Task<Player[]> GetAll(){
            var allPlayers = await ApiUtility.GetPlayerCollection().Find(_ => true).ToListAsync();
            return allPlayers.Select(player => player)
                .Where(playerDe => !playerDe.IsDeleted).ToArray();
        }

        public async Task<Player> Create(string name){
            Console.WriteLine("Sending created player to client");
            var player = new NewPlayer(name).SetupNewPlayer(new Player());
            Console.WriteLine(player.Id);
            await SendPlayerDataToMongo(player);
            return player;
        }

        async Task SendPlayerDataToMongo(Player player){
            await ApiUtility.GetPlayerCollection().InsertOneAsync(player);
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            var filter = Db.GetPlayerById(id);

            var update = Builders<Player>.Update
                .Set(x => x.Gold, modifiedPlayer.Gold)
                .Set(x => x.Score, modifiedPlayer.Score)
                .Set(x => x.Level, modifiedPlayer.Level);

            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(filter, update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After
                });
        }


        public async Task<Player> Delete(Guid id){
            var update = Builders<Player>.Update.Set("IsDeleted", true);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(Db.GetPlayerById(id), update,
                new FindOneAndUpdateOptions<Player>(){
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Player> CreateItem(Guid id, string itemName, ItemTypes itemType){
            if (!IsCorrectItemType(itemType)){
                throw new Exception("Item type does not exist");
            }

            var item = new Item(itemName, itemType.ToString());

            var filter = Builders<Player>.Filter.Eq(new StringFieldDefinition<Player, Guid>(nameof(Player.Id)), id);
            var update = Builders<Player>.Update.Push(x => x.Inventory, item);
            return await ApiUtility.GetPlayerCollection().FindOneAndUpdateAsync(filter, update,
                new FindOneAndUpdateOptions<Player>{
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = true
                });
        }

        static bool IsCorrectItemType(ItemTypes itemType){
            return Enum.IsDefined(typeof(ItemTypes), itemType.ToString());
        }

        public async Task<Item> DeleteItem(Guid id, string itemName){
            var newFilter = Builders<Player>.Filter.And(Builders<Player>.Filter.Where(x => x.Id == id),
                Builders<Player>.Filter.ElemMatch(x => x.Inventory, x => x.ItemName == itemName));

            var update = Builders<Player>.Update.Set("Inventory.$.IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(newFilter, update);
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
                .UpdateOneAsync(Db.GetPlayerById(id), update, new UpdateOptions{IsUpsert = true});
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
                .UpdateOneAsync(Db.GetPlayerById(id), update, new UpdateOptions{IsUpsert = true});
            return item;
        }

        async Task UnEquip(Guid id, string name){
            var item = await GetItem(id, name);
            var update = Builders<Player>.Update
                .Set(x => x.EquippedItems[item.ItemType], null)
                .Inc(player => player.Level, -item.LevelBonus);

            await ApiUtility.GetPlayerCollection().UpdateOneAsync(Db.GetPlayerById(id), update);
        }

        static bool IsNullOrWrongType(ItemTypes type, Item item){
            return item == null || item.ItemType != type.ToString();
        }
    }
}