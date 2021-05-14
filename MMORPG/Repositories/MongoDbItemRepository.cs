using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Api;
using MMORPG.Data;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Repositories{
    public class MongoDbItemRepository : IItemRepository{
        public static IRepository Repository => new MongoDbRepository();

        public async Task<Item> CreateItem(Guid id, ModifiedItem newItem){
            if (!IsCorrectItemType(newItem.ItemType)){
                throw new NotFoundException("Item type does not exist");
            }

            return await new NewItem(newItem.ItemName, newItem.ItemType, newItem.LevelRequirement,
                newItem.LevelBonus, newItem.Rarity, newItem.SellValue).CreateItem(id);
        }

        static bool IsCorrectItemType(ItemTypes itemType){
            return Enum.IsDefined(typeof(ItemTypes), itemType.ToString());
        }

        public async Task<Item> DeleteItem(Guid playerId, string itemName){
            var filter = Builders<Player>.Filter.And(playerId.GetPlayerById(),
                Builders<Player>.Filter.ElemMatch(x => x.Inventory, x => x.ItemName == itemName));

            var update = Builders<Player>.Update.Set("Inventory.$.IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(filter, update);
            return await GetItem(playerId, itemName);
        }

        public async Task<List<Item>> GetInventory(Guid id){
            try{
                var inventory = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).SingleAsync();
                return inventory.Inventory.Where(x => !x.IsDeleted).Select(item => item).ToList();
            }
            catch (Exception){
                throw new NotFoundException("Player not found or has been deleted ");
            }
        }

        public async Task<Item> GetItem(Guid id, string name){
            var inventory = await GetInventory(id);
            return inventory.Find(item => item.ItemName == name);
        }

        public async Task<Item> SellItem(Guid playerId, string itemName){
            var item = await GetItem(playerId, itemName);
            var update = Builders<Player>.Update.Inc(x => x.Gold, item.SellValue);
            await Repository.EquipRepository.UnEquip(playerId, item);
            await Repository.PlayerRepository.UpdatePlayer(playerId, update);
            return await DeleteItem(playerId, itemName);
        }
    }
}