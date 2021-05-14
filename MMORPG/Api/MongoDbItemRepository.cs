using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.Database;
using MMORPG.Exceptions;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Api{
    public class MongoDbItemRepository : IItemRepository{
        public async Task<Player> CreateItem(Guid id, ModifyItem modifyItem){
            if (!IsCorrectItemType(modifyItem.ItemType)){
                throw new NotFoundException("Item type does not exist");
            }

            var item = new Item(modifyItem.ItemName, modifyItem.ItemType, modifyItem.LevelRequirement,
                modifyItem.LevelBonus, modifyItem.Rarity, modifyItem.SellValue);

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
                var inventory = await ApiUtility.GetPlayerCollection().Find(id.GetPlayerById()).SingleAsync();
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
                .FindAsync(x => x.EquippedItems[item.ItemType.ToString()] == item);
            
            //TODO fix this shit
            // if (equippedItems != null)
            //     await UnEquip(id, itemName);

            var update = Builders<Player>.Update.Inc(x => x.Gold, item.SellValue);
            await ApiUtility.GetPlayerCollection()
                .UpdateOneAsync(id.GetPlayerById(), update, new UpdateOptions{IsUpsert = true});
            return await DeleteItem(id, itemName);
        }
    }
}