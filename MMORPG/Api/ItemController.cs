using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Items;
using MMORPG.Players;
using MMORPG.Utilities;
using MongoDB.Driver;

namespace MMORPG.Api{
    [ApiController]
    [Route("players/{id:guid}/items")]
    public class ItemController : IItemController{
        [HttpPost("CreateItem")]
        public async Task<IItem> CreateItem(Guid id, string itemName, ItemTypes itemType){
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

        [HttpDelete("DeleteItem")]
        public async Task<IItem> DeleteItem(Guid id, string itemName){
            var newFilter = Builders<Player>.Filter.And(Builders<Player>.Filter.Where(x => x.Id == id.ToString()),
                Builders<Player>.Filter.ElemMatch(x => x.Inventory, x => x.ItemName == itemName));

            var update = Builders<Player>.Update.Set("Inventory.$.IsDeleted", true);
            await ApiUtility.GetPlayerCollection().UpdateOneAsync(newFilter, update);
            return default;
        }
    }
}