using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Items;
using MMORPG.Utilities;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MMORPG.Api{
    [ApiController]
    [Route("items")]
    public class ItemController : IItemController{
        [HttpPost("Generate{itemType}/{itemName}")]
        public async Task<IItem> CreateItem(string itemType, string itemName){
            var item = new Item();
            if (itemType is not ("Armor" or "Weapon" or "Helmet")){
                throw new Exception("Item type does not exist");
            }

            item.ItemName = itemName;
            item.ItemType = itemType;
            var serializedPlayer = JsonConvert.SerializeObject(item);
            var bsonDocument = BsonDocument.Parse(serializedPlayer);
            await ApiUtility.GetItemCollection().InsertOneAsync(bsonDocument);
            return item;
        }
    }
}