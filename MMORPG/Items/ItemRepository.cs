using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMORPG.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MMORPG.Items{
    [ApiController]
    [Route("items")]
    public class ItemRepository : IItemRepository{
        readonly IMongoCollection<BsonDocument> collection;

        public ItemRepository(){
            this.collection = DatabaseConnection.GetDatabase().GetCollection<BsonDocument>("Items");
        }

        [HttpGet("Create{itemType} {itemName}")]
        public async Task<IItem> Create<T>(string itemType, string itemName) where T : new(){
            var itemsTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(p => typeof(IItem).IsAssignableFrom(p));

            if (itemsTypes.Any(type => type.Name == itemType)){
                return await GenerateItem<T>(itemName);
            }

            throw new Exception("Item type does not exist");
        }

        async Task<IItem> GenerateItem<T>(string itemName) where T : new(){
            var itemType = new T();
            var item = (IItem) Convert.ChangeType(itemType, typeof(T));
            item.ItemName = itemName;
            var serializedPlayer = JsonConvert.SerializeObject(item);
            var bsonDocument = BsonDocument.Parse(serializedPlayer);
            await this.collection.InsertOneAsync(bsonDocument);
            return item;
        }
    }
}