using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Exceptions;
using MMORPG.Items;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Database{
    public class ItemRepository : IItemRepository{
        public async Task<Item> CreateItem(Guid id, string itemName, ItemTypes itemType){
            if (!IsCorrectItemType(itemType)){
                throw new Exception("Item type does not exist");
            }

            var item = new Item(itemName, itemType.ToString());
            var filter = Builders<Player>.Filter.Eq(x => x.Id, id.ToString());
            var update = Builders<Player>.Update.Push(x => x.Inventory, item);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = true});
            return item;
        }

        static bool IsCorrectItemType(ItemTypes itemType){
            return Enum.IsDefined(typeof(ItemTypes), itemType.ToString());
        }

        public async Task<Item> DeleteItem(Guid id, string itemName){
            var newFilter = Builders<Player>.Filter.And(Builders<Player>.Filter.Where(x => x.Id == id.ToString()),
                Builders<Player>.Filter.ElemMatch(x => x.Inventory, x => x.ItemName == itemName));

            var update = Builders<Player>.Update.Set("Inventory.$.IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(newFilter, update);
            return default;
        }

        public async Task<List<Item>> GetInventory(Guid id){
            try{
                var filter = Builders<Player>.Filter.Eq(x => x.Id, id.ToString());
                var inventory = await ApiUtility.GetPlayerCollection().Find(filter).SingleAsync();
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
            var filter = Builders<Player>.Filter.Eq(x => x.Id, id.ToString());
            var update = Builders<Player>.Update.Inc(x => x.Gold, item.SellValue);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(filter, update, new UpdateOptions{IsUpsert = true});
            return await DeleteItem(id, itemName);
        }
    }
}